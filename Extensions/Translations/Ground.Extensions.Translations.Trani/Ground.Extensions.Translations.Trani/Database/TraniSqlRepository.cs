﻿
using Dapper;
using Ground.Extensions.Translations.Trani.DataModel;
using Ground.Extensions.Translations.Trani.Options;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Ground.Extensions.Translations.Trani.Database
{

    
    public class TraniSqlRepository
    {
        private readonly IDbConnection _dbConnection;
        private List<LocalizationRecord> _localizationRecords;

        static readonly object _locker = new();

        private readonly TraniTranslatorOptions _configuration;
        private readonly ILogger _logger;
        private readonly string _selectCommand = "Select * from [{0}].[{1}]";
        private readonly string _insertCommand = "INSERT INTO [{0}].[{1}]([Key],[Value],[Culture]) VALUES (@Key,@Value,@Culture) select SCOPE_IDENTITY()";

        public TraniSqlRepository(TraniTranslatorOptions configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            _dbConnection = new SqlConnection(configuration.ConnectionString);

            if (_configuration.AutoCreateSqlTable)
                CreateTableIfNeeded();

            _selectCommand = string.Format(_selectCommand, configuration.SchemaName, configuration.TableName);

            _insertCommand = string.Format(_insertCommand, configuration.SchemaName, configuration.TableName);

            LoadLocalizationRecords(null);

            SeedData();

            LoadLocalizationRecords(null);

            _ = new Timer(LoadLocalizationRecords,
                               null,
                               TimeSpan.FromMinutes(configuration.ReloadDataIntervalInMinuts),
                               TimeSpan.FromMinutes(configuration.ReloadDataIntervalInMinuts));

        }

        private void CreateTableIfNeeded()
        {
            try
            {
                string table = _configuration.TableName;
                string schema = _configuration.SchemaName;

                _logger.LogInformation("Trani Translator tries to create table in database. Schema name is {Schema}. Table name is {TableName}", schema, table);


                string createTable = $"IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE " +
                    $"TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' )) Begin " +
                    $"CREATE TABLE [{schema}].[{table}]( " +
                    $"Id bigint  Primary Key Identity(1,1)," +
                    $"[BusinessId] [UNIQUEIDENTIFIER] NOT NULL UNIQUE  default(newId())," +
                    $"[Key] [nvarchar](255) NOT NULL," +
                    $"[Value] [nvarchar](500) NOT NULL," +
                    $"[Culture] [nvarchar](5) NULL)" +
                    $" End";
                _dbConnection.Execute(createTable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create table for Trani Translator Failed");
                throw;
            }

        }

        private void LoadLocalizationRecords(object? state)
        {
            lock (_locker)
            {
                _logger.LogInformation("Trani Translator loads localization recored at {DateTime}", DateTime.Now);

                _localizationRecords = _dbConnection.Query<LocalizationRecord>(_selectCommand, commandType: CommandType.Text).ToList();

                _logger.LogInformation("Trani Translator loaded localization recored at {DateTime}. Total record count is {RecordCount}", DateTime.Now, _localizationRecords.Count);
            }
        }

        private void SeedData()
        {
            string values = string.Empty;

            var newItemsInDefaultTranslations = _configuration.DefaultTranslations.
                Where(c => !_localizationRecords.Any(d => d.Key.Equals(c.Key) && d.Culture.Equals(c.Culture)))
                .Select(c => $"(N'{c.Key}',N'{c.Value}',N'{c.Culture}')").ToList();

            var count = newItemsInDefaultTranslations.Count;
            if (count > 0)
            {
                string _command = $"INSERT INTO [{_configuration.SchemaName}].[{_configuration.TableName}]([Key],[Value],[Culture]) VALUES {string.Join(",", newItemsInDefaultTranslations)}";

                using IDbConnection db = new SqlConnection(_configuration.ConnectionString);

                db.Execute(_command, commandType: CommandType.Text);

                _logger.LogInformation("Trani Translator Adds {Count} items to its dictionary from default translations at {DateTime}", count, DateTime.Now);

            }
        }

        public string Get(string key, string culture)
        {
            try
            {
                var record = _localizationRecords.FirstOrDefault(c => c.Key == key && c.Culture == culture);
                if (record == null)
                {
                    _logger.LogInformation("The key was not found and was registered with the default value in Trani Translator. Key is {Key} and culture is {Culture}", key, culture);
                    record = new LocalizationRecord
                    {
                        Key = key,
                        Culture = culture,
                        Value = key
                    };

                    var parameters = new DynamicParameters();
                    parameters.Add("@Key", key);
                    parameters.Add("@Culture", culture);
                    parameters.Add("@Value", key);

                    record.Id = _dbConnection.Query<int>(_insertCommand, param: parameters, commandType: CommandType.Text).FirstOrDefault();
                    _localizationRecords.Add(record);
                }
                return record.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get Key value from Sql Server for Trani Translator Failed");
                throw;
            }

        }
    }
}

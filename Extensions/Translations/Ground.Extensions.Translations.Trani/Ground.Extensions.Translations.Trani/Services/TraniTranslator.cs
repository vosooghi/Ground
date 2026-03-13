using Microsoft.Extensions.Logging;
using System.Globalization;
using Ground.Extensions.Translations.Abstractions;
using Ground.Extensions.Translations.Trani.Database;
using Ground.Extensions.Translations.Trani.Options;
using Microsoft.Extensions.Options;
namespace Ground.Extensions.Translations.Trani.Services
{
    /// <summary>
    /// An implementation of ITranslator interface that uses a SQL Server database to store and retrieve localization records. It loads all the localization records into memory and provides methods to get the value of a key for a specific culture. If the key is not found, it adds the key with the default value to the database and returns the key as the value. It also provides methods to get a formatted string with arguments and to concatenate multiple keys with a separator. The translator is culture-aware and uses the current culture of the application to determine which localization record to return.
    /// </summary>
    public class TraniTranslator : ITranslator, IDisposable
    {
        private readonly string _currentCulture;
        private readonly TraniSqlRepository _localizer;
        private readonly ILogger<TraniTranslator> _logger;

        public TraniTranslator(IOptions<TraniTranslatorOptions> configuration, ILogger<TraniTranslator> logger)
        {
            _currentCulture = CultureInfo.CurrentCulture.ToString();
            _logger = logger;
            if (string.IsNullOrWhiteSpace(_currentCulture))
            {
                _currentCulture = "en-US";
                _logger.LogInformation("Trani Translator current culture is null and set to en-US");
            }
            _localizer = new TraniSqlRepository(configuration.Value, logger);
            _logger.LogInformation("Trani Translator Start working with culture {Culture}", _currentCulture);
        }

        public string this[string name] { get => GetString(name); set => throw new NotImplementedException(); }


        public string this[string name, params string[] arguments] { get => GetString(name, arguments); set => throw new NotImplementedException(); }


        public string this[char separator, params string[] names] { get => GetConcateString(separator, names); set => throw new NotImplementedException(); }


        public string GetString(string name)
        {
            _logger.LogTrace("Trani Translator GetString with name {name}", name);

            return _localizer.Get(name, _currentCulture);
        }


        public string GetString(string pattern, params string[] arguments)
        {
            _logger.LogTrace("Trani Translator GetString with pattern {pattern} and arguments {arguments}", pattern, arguments);

            for (int i = 0; i < arguments.Length; i++)
            {
                arguments[i] = GetString(arguments[i]);
            }

            pattern = GetString(pattern);

            for (int i = 0; i < arguments.Length; i++)
            {
                string placeHolder = $"{{{i}}}";
                pattern = pattern.Replace(placeHolder, arguments[i]);
            }

            return pattern;
        }

        public string GetConcateString(char separator = ' ', params string[] names)
        {
            _logger.LogTrace("Trani Translator GetConcateString with separator {separator} and names {names}", separator, names);

            for (int i = 0; i < names.Length; i++)
            {
                names[i] = GetString(names[i]);
            }

            return string.Join(separator, names);
        }

        public void Dispose()
        {
            _logger.LogInformation("Trani Translator Stop working with culture {Culture}", _currentCulture);
        }
    }
}

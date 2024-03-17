using Ground.Extensions.DependencyInjection;
using Ground.Extensions.Translations.Trani.Options;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// Trani translator needs database configs
/// Database reload time
/// the Database should be created before.
/// </summary>
TraniTranslatorOptions options = new TraniTranslatorOptions();
DefaultTranslationOption[] translationOption = new DefaultTranslationOption[]
{
    new DefaultTranslationOption(){Key = "TITLE", Value = "ÚäæÇä", Culture = "fa-IR" },
    new DefaultTranslationOption(){ Key = "TITLE", Value = "Title", Culture = "en-US" }
};
builder.Services.AddGroundTraniTranslator(c =>
{
    c.ConnectionString = "Server=.; Initial Catalog=TraniSampleDb; User Id=sa; Password=P@ssw0rd;encrypt=false";
    c.AutoCreateSqlTable = true;
    c.SchemaName = "dbo";
    c.TableName = "TraniTranslations";
    c.ReloadDataIntervalInMinuts = 1;
    c.DefaultTranslations = translationOption;
    //[C# 12
    //    new() { Key = "TITLE", Value = "ÚäæÇä", Culture = "fa-IR" },
    //    new() { Key = "TITLE", Value = "Title", Culture = "en-US" },
    //];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

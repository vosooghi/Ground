using Ground.Utilities.OpenTelemetryRegistration.Sample.Models;
using Microsoft.EntityFrameworkCore;

using Ground.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PersonContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//Config and add OpentTelemetry with config file
builder.Services.AddGroundObservabilitySupport(configuration);

//builder.Services.AddGroundObservabilitySupport(c =>
//{
//    c.ApplicationName = "Ground";
//    c.ServiceName = "OpenTelemetrySample";
//    c.ServiceVersion = "1.0.0";
//    c.ServiceId = "ab387bb6-9a66-444f-92b2-ff64e2a81f96";
//    c.OltpEndpoint = "http://localhost:4317";
//    c.ExportProcessorType = OpenTelemetry.ExportProcessorType.Simple;
//    c.SamplingProbability = 1;
//});
var app = builder.Build();


app.UseGroundObservabilityMiddlewares();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

SeedData.EnsurePopulate(app);

app.Run();
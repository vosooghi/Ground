using Ground.Utilities.SerilogRegistration.Extensions;
using Ground.Extensions.DependencyInjection;
using Ground.Utilities.SerilogRegistration.Sample;
using Ground.Utilities.SerilogRegistration.Sample.SampleEnrichers;
SerilogExtensions.RunWithSerilogExceptionHandling(() =>
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.AddGroundSerilog(c =>
    {
        c.ApplicationName = "SerilogRegistration";
        c.ServiceName = "SampleService";
        c.ServiceVersion = "1.0";
        c.ServiceId = Guid.NewGuid().ToString();
    }, typeof(Sample01Enricher), typeof(Sample02Enricher)).ConfigureServices().ConfigurePipeline();
    app.Run();
});
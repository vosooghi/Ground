using Ground.Core.Contracts.ApplicationServices.Commands;
using Ground.Core.Contracts.ApplicationServices.Events;
using Ground.Extensions.DependencyInjection;
using Ground.Extensions.MessageBus.MessageInbox.Sample.CommandDispatcher;
using Ground.Extensions.MessageBus.MessageInbox.Sample.EventDisptacher;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICommandDispatcher, CommandDistpacher>();
builder.Services.AddScoped<IEventDispatcher, EventDistpacher>();

builder.Services.AddGroundNewtonSoftSerializer();
builder.Services.AddGroundRabbitMqMessageBus(c =>
{
    c.PerssistMessage = true;
    c.ExchangeName = "NewsCmsExchange";//"MiniBlogExchange";
    c.ServiceName = "BasicInfo";//"SampleApplciatoinReceiver";
    c.Url = "localhost";//@"amqp://guest:guest@localhost:5672/";
});

builder.Services.AddGroundMessageInbox(c =>
{
    c.ApplicationName = "SampleApplciatoinReceiver";
    //c.ConnectionString = "Server=.;Initial Catalog=InboxDb;User Id=sa; Password=1qaz!QAZ;Encrypt=false";
});

builder.Services.AddGroundMessageInboxDalSql(c =>
{
    //c.TableName = "MessageInbox";
    c.SchemaName = "dbo";
    c.ConnectionString = "Server=.;Initial Catalog=InboxDb;User Id=sa; Password=P@ssw0rd;Encrypt=false";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("BasicInfo", "KeywordCreated"));

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Ground.Extensions.DependencyInjection;
using Ground.Extensions.MessageBus.Abstractions;
using Ground.Extensions.MessageBus.RabbitMQ.Sample.BackgroundServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGroundNewtonSoftSerializer();
builder.Services.AddGroundRabbitMqMessageBus(c =>
{
    c.PerssistMessage = true;
    c.ExchangeName = "SampleExchange";
    c.ServiceName = "SampleApplciatoin";
    c.Url = "localhost";//@"amqp://guest:guest@localhost:5672/";
});
builder.Services.AddScoped<IMessageConsumer, ReceivedMessage>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("SampleApplciatoin", "PersonEvent"));
app.Services.ReceiveCommandFromRabbitMqMessageBus("PersonCommand");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

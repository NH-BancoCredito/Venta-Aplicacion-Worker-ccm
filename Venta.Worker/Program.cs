using VentaWorker.Api.Middleware;
using VentaWorker.Infrastructure;
using VentaWorker.Worker.Workers;
using VentaWorker.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Capa de aplicacion
builder.Services.AddApplication();

//Capa de infra
var connectionString = builder.Configuration.GetConnectionString("dbStocks-cnx");
builder.Services.AddInfraestructure(connectionString);
//Adiconando el background service
builder.Services.AddHostedService<ActualizarStocksWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Adicionar middleware customizado para tratar las excepciones
app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();

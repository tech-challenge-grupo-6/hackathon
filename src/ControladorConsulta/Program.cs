using ControladorConsulta.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(opitions => opitions.UseInMemoryDatabase("ControladorConsulta"));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/hello", () =>
{
    return "Hello World!";
})
.WithName("HelloWorld")
.WithOpenApi();

app.Run();


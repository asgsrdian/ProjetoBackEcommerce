using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados na API
// builder.Services.AddDbContext<AppDbContext>(
//     options => options.UseInMemoryDatabase("tarefas"));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<Bancodedados>();


//Configuração Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Projeto");

//APIs
app.MapClientesApi();
app.MapProdutosApi();
app.MapAdministradorApi();
app.MapCarrinhosApi();
app.MapEnderecosApi();

app.Run();


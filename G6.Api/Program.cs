using Microsoft.OpenApi.Models;
using G6.Application.Mapping;
using G6.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using G6.Application.Interfaces;
using G6.Application.Services;
using G6.Domain.Interfaces;
using G6.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "G6Bank", Version = "v1" });
});
// Registro do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DataBase");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IAtivosRepository, AtivosRepository>();

// Adiciona o serviço AtivosService
builder.Services.AddScoped<IAtivosService, AtivosService>();
builder.Services.AddScoped<IDadosHistoricosRepository, DadosHistoricosRepository>();

builder.Services.AddHostedService<RotinaAutomaticaBrapiAPI>();

// Registro do AutoMapper
builder.Services.AddAutoMapper(typeof(DomainToDTOMappingProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.

 app.UseSwagger();
 app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

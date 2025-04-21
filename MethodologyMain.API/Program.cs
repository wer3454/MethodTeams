using MethodTeams.Data;
using MethodTeams.Services;
using System.Text.Json.Serialization;
using MethodologyMain.Application.Interface;
using MethodologyMain.Application.Services;
using MethodologyMain.API.Middleware;
using AuthMetodology.Infrastructure.Models;
using AuthMetodology.Infrastructure.Interfaces;
using MethodologyMain.Infrastructure.Services;
using MethodologyMain.Persistence.Interfaces;
using MethodologyMain.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(nameof(RabbitMqOptions)));

builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamValidationService, TeamValidationService>();
builder.Services.AddSingleton<ILogQueueService, LogQueueService>();
builder.Services.AddSingleton<IRabbitMqService,RabbitMqService>();
builder.Services.AddDbContext<MyDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();

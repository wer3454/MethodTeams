using MethodTeams.Data;
using MethodTeams.Services;
using AutoMapper;
using System.Text.Json.Serialization;
using MethodologyMain.Application.Interface;
using MethodologyMain.Application.Services;
using MethodologyMain.API.Middleware;
using AuthMetodology.Infrastructure.Models;
using AuthMetodology.Infrastructure.Interfaces;
using MethodologyMain.Infrastructure.Services;
using MethodologyMain.Persistence.Interfaces;
using MethodologyMain.Persistence.Repository;
using MethodTeams.DTO;
using System.Reflection;
using MethodologyMain.Application.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(static cfg =>
{
    cfg.AddMaps(Assembly.GetExecutingAssembly());
    cfg.AllowNullCollections = true;
    cfg.AddGlobalIgnore("Item");
}
);
IMapper mapper = mapperConfig.CreateMapper();

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(nameof(RabbitMqOptions)));

builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddAutoMapper(typeof(TeamProfile).Assembly, typeof(TeamInfoDto).Assembly);
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamValidationService, TeamValidationService>();

builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton<ILogQueueService, LogQueueService>();
builder.Services.AddSingleton<IRabbitMqService,RabbitMqService>();

var connection = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<MyDbContext>(opt => opt.UseNpgsql(connection));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();
//app.UseAuthentication();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();

using MethodTeams.Data;
using MethodTeams.Services;
using AutoMapper;
using System.Text.Json.Serialization;
using MethodologyMain.Application.Interface;
using MethodologyMain.Application.Services;
using MethodologyMain.API.Middleware;
using MethodologyMain.Infrastructure.Services;
using MethodologyMain.Persistence.Interfaces;
using MethodologyMain.Persistence.Repository;
using System.Reflection;
using MethodologyMain.Application.Profiles;
using Microsoft.EntityFrameworkCore;
using RabbitMqModel.Models;
using RabbitMqPublisher.Interface;
using AuthMetodology.Infrastructure.Models;
using MethodologyMain.Infrastructure.Models;
using MethodologyMain.API.Extensions;
using Microsoft.Extensions.Options;
using RabbitMqListener.Interfaces;
using MethodologyMain.Infrastructure.Listeners;
using MethodologyMain.Application.DTO;
using MethodologyMain.Logic.Entities;
using Serilog;
using MethodologyMain.Logic.Models;
using MethodologyMain.Infrastructure.Interfaces;
using Prometheus;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new MapperConfiguration(static cfg =>
{
    cfg.AddMaps(Assembly.GetExecutingAssembly());
    cfg.CreateMap<TeamEntity, GetTeamDto>()
    .ForMember(dto => dto.CreatedBy, conf => conf.MapFrom(t => t.CaptainId))
    .ForMember(dto => dto.Members, conf => conf.MapFrom(t => t.Members.Select(s => s.User.UserName).ToList()))
    .ForMember(dto => dto.Tags, conf => conf.MapFrom(t => t.Tags.Select(s => s.Tag.TagName).ToList()))
    .ForMember(dto => dto.CreatedAt, conf => conf.MapFrom(t => t.TeamCreatedAt));
    cfg.CreateMap<UserMainEntity, GetUserDto>()
    .ForMember(dto => dto.Tags, conf => conf.MapFrom(t => t.Tags.Select(s => s.Tag.TagName).ToList()))
    .ForMember(dto => dto.Name, conf => conf.MapFrom(t => t.UserName))
    .ForMember(dto => dto.Bio, conf => conf.MapFrom(t => t.Education));
    cfg.CreateMap<HackathonEntity, GetHackathonDto>()
    .ForMember(dto => dto.Tags, conf => conf.MapFrom(t => t.Tags.Select(s => s.Tag.TagName).ToList()))
    .ForMember(dto => dto.TeamSize, conf => conf.MapFrom(t => new TeamSize { Max = t.MaxTeamSize, Min = t.MinTeamSize }))
    .ForMember(dto => dto.OrganizerLogo, conf => conf.MapFrom(t => t.Organization.Logo))
    .ForMember(dto => dto.OrganizerName, conf => conf.MapFrom(t => t.Organization.Name));
    cfg.CreateMap<OrganizationEntity, GetOrganizationDto>();

    cfg.AllowNullCollections = true;
    cfg.AddGlobalIgnore("Item");
}
);
configuration.AssertConfigurationIsValid();
IMapper mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(nameof(RabbitMqOptions)));
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection(nameof(JWTOptions)));

builder.Services.AddApiAuthentication(builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JWTOptions>>());

builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IHackathonService, HackathonService>();
builder.Services.AddAutoMapper(typeof(TeamProfile).Assembly, typeof(TeamInfoDto).Assembly);
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IHackathonRepository, HackathonRepository>();
builder.Services.AddScoped<ITeamValidationService, TeamValidationService>();
builder.Services.AddSingleton<IRabbitMqPublisherBase<RabbitMqLogPublish>, LogQueueService>();
builder.Services.AddHostedService<RabbitMqUserRegisterListener>()
    .AddSingleton<IRabbitMqListenerBase, RabbitMqUserRegisterListener>();
builder.Services.AddScoped<IRedisService, RedisService>();
var connection = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<MyDbContext>(opt => opt.UseNpgsql(connection));
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseMetricServer();
app.UseHttpMetrics(options =>
{
    // This will preserve only the first digit of the status code.
    // For example: 200, 201, 203 -> 2xx
    options.ReduceStatusCodeCardinality();
});
app.UseRequestMiddleware();

app.UseCors("AllowFrontend");
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();

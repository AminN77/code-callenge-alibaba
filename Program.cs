using CodeChallenge.DAL;
using CodeChallenge.BackgroundServices;
using CodeChallenge.ExternalServices;
using CodeChallenge.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CodeChallenge.Middlewares;
using CodeChallenge.Attributes;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<StandardResponseAttribute>();
builder.Services.AddScoped<WeatherLogicManager>();
builder.Services.AddScoped<WeatherAPIService>();
builder.Services.AddSingleton<DataQueue>();
builder.Services.AddHostedService<WeatherUpdaterService>();
builder.Services.AddScoped<Repository>();
builder.Services.AddDbContext<PgSqlDbContext>
    (opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("Docker-PgSqlConnection")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

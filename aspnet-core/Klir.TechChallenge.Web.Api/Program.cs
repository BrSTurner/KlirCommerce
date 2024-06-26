using Klir.TechChallenge.Infra.IoC;
using Klir.TechChallenge.Web.Api.Configuration;
using Klir.TechChallenge.Web.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

const string ALLOW_SPECIFIC_ORIGINS = "_allowSpecificOrigins";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddCors(options => options.AddPolicy(ALLOW_SPECIFIC_ORIGINS, builder =>
{
    builder
    .AllowAnyHeader()  
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200");
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(ALLOW_SPECIFIC_ORIGINS);

app.MapEndpoints();

app.UseMiddleware<NotificationMiddleware>();

app.Run();

public partial class Program { }
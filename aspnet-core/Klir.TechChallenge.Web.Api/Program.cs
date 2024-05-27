using Klir.TechChallenge.Infra.IoC;
using Klir.TechChallenge.Web.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

const string ALLOW_SPECIFIC_ORIGINS = "_allowSpecificOrigins";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ResolveDependencies(builder.Configuration);
builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddCors(options => options.AddPolicy(ALLOW_SPECIFIC_ORIGINS, builder =>
{
    builder.WithOrigins("http://localhost:4200");
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

app.Run();

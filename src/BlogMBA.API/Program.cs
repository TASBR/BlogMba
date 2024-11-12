using ApiFuncional.Configuration;
using BlogMBA.API.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder
    .AddApiConfig()
    .AddCorsConfig()
    .AddSwaggerConfig()
    .AddDbContextConfig()
    .AddIdentityConfig();

builder.Services.AddScoped<IGerarJWT, GerarJWT>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

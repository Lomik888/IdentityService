using DotNetEnv;
using IdentityService.API;
using IdentityService.API.Middlewear;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Services.ConfigureServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/Error");

app.Run();
using IdentityContext.Application.Configuration.Authentification;
using MediaContext.Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services
    .RegisterMediaContext(null);

builder
    .Services
    .AddJWTAuthentication(null)
    .AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var application = scope.ServiceProvider.GetRequiredService<BackendMedia.Application>();
    await application.Start();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app
    .UseCors(builder => builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

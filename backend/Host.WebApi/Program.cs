
using Host.WebApi.Configuration;
using Host.WebApi.Web.JsonConverters;
using IdentityContext.Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var services = builder.Services;

services
    .AddControllers()
    .AddModuleControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

services
    .RegisterSwagger();

services
    .AddEndpointsApiExplorer();

services
    .RegisterHostServices()
    .RegisterWebServices()
    .RegisterModuleServices(config);

services
    .AddJWTAuthentication(config)
    .AddAuthorization();

var app = builder.Build();

var dbInit = new ApplicationDbInitializer(app.Services);
await dbInit.Initialize();

app.UseSwagger();
app.UseSwaggerUI();

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

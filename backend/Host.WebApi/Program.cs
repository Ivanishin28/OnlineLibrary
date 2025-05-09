
using Host.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var services = builder.Services;

services
    .AddControllers()
    .AddModuleControllers();

services
    .RegisterSwagger();

services.AddEndpointsApiExplorer();

services
    .RegisterModuleServices(config);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

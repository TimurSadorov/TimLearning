using Microsoft.EntityFrameworkCore;
using TimLearning.Api.Configurations;
using TimLearning.Application.Configurations;
using TimLearning.Infrastructure.Implementation.Configurations;
using TimLearning.Infrastructure.Interfaces.Db;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

services.AddAllApiServices(config);
services.AddAllInfrastructureServices(config);
services.AddAllApplicationServices(config);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseTimLearningSwaggerAndUI();
}

app.UseHttpsRedirection();

app.UseTimLearningAuthentication();
app.UseTimLearningAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

    await db.Database.MigrateAsync();
}

app.Run();

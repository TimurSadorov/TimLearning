using Microsoft.EntityFrameworkCore;
using TimLearning.Api.Configurations;
using TimLearning.Application.Configurations;
using TimLearning.Domain.Configurations;
using TimLearning.Infrastructure.Implementation.Configurations;
using TimLearning.Infrastructure.Implementation.Configurations.Options;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

var siteOptions = config.GetRequiredConfig<TimLearningSiteOptions>();
services.AddAllApiServices(config, siteOptions.Url);

services.AddAllInfrastructureServices(
    config,
    dbOptions =>
    {
        if (builder.Environment.IsDevelopment())
        {
            dbOptions.EnableSensitiveDataLogging();
        }
    }
);

services.AddAllApplicationServices(config);

services.AddAllDomainServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseTimLearningSwaggerAndUI();
}

app.UseRouting();

app.UseCors();

app.UseTimLearningAuthentication();
app.UseTimLearningAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

    await db.Database.MigrateAsync();
}

app.Run();

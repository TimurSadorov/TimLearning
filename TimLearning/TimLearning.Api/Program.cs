using Microsoft.EntityFrameworkCore;
using TimLearning.Model.Configurations;
using TimLearning.Model.Db;
using TimLearning.Shared.AspNet.Auth.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

services.AddControllers();

services.AddTimLearningAuthentication(config);
services.AddTimLearningAuthorization();

services.AddSwaggerGen();

services.AddModelServices(config);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseTimLearningAuthentication();
app.UseTimLearningAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await db.Database.MigrateAsync();
}

app.Run();

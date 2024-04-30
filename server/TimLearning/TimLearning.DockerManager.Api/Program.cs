using Quartz;
using Quartz.AspNetCore;
using TimLearning.DockerManager.Api.Jobs;
using TimLearning.DockerManager.Api.Services;
using TimLearning.DockerManager.Api.Services.Docker;
using TimLearning.Shared.AspNet.Swagger;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();

var useSwagger = builder.Environment.IsDevelopment();
if (useSwagger)
{
    services.AddTimLearningSwaggerGen();
}

services.AddDocker();
services.AddSingleton<IAppTester, AppTester>();

services.AddQuartz(quartzConfigurator =>
{
    var jobName = new JobKey(nameof(ContainerCleanerJob));
    quartzConfigurator.AddJob<ContainerCleanerJob>(jobName);
    quartzConfigurator.AddTrigger(
        c =>
            c.ForJob(jobName)
                .StartAt(DateTimeOffset.Now + TimeSpan.FromMinutes(5))
                .WithSchedule(SimpleScheduleBuilder.RepeatMinutelyForever(5))
    );
});
services.AddQuartzServer();

var app = builder.Build();

if (useSwagger)
{
    app.UseTimLearningSwaggerAndUI();
}

app.UseRouting();

app.MapControllers();

app.Run();

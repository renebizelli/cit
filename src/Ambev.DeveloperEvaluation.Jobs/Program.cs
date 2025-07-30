using Ambev.DeveloperEvaluation.Jobs;
using Ambev.DeveloperEvaluation.ORM;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Starting jobs");

builder.Services.AddScheduler();

builder.Services.AddTransient<RecalculateProductRatingsJob>();

builder.Services.AddDbContext<DefaultContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
    )
);

builder.RegisterDependencies();

var app = builder.Build();

app.Services.UseScheduler(s =>
{
    s.Schedule<RecalculateProductRatingsJob>()
        .EveryMinute()
        .RunOnceAtStart();
});

await app.RunAsync();



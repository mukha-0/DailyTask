using DailyTask.DataAccess;
using DailyTask.DataAccess.Contexts;
using DailyTask.DataAccess.Repositories;
using DailyTask.DataAccess.UnitOfWork;
using DailyTask.Service.Services.DailyTask;
using DailyTask.Service.Services.DailyTask.Models;

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ExamProject.Middlewares;
using DailyTask.Service.Validators.DailyTask;
using DailyTask.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration)
                 .Enrich.FromLogContext();
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSQLConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/dailytask-.log", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

Log.Information("Daily Task application started successfully!");
Log.Warning("SEQ TEST WARNING — If you see this in Seq, everything works.");
    
builder.Host.UseSerilog();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IDailyTask, DailyTaskk>();

builder.Services.AddValidatorsFromAssemblyContaining<DailyTaskCreateModelValidator>();

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=DailyTask}/{action=Index}/{id?}");

Console.WriteLine(builder.Configuration.GetConnectionString("PostgresSQLConnection"));

app.Run();

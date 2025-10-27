using Contracts.Interfaces;
using Data.Context;
using Data.Models;
using Data.Repos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using StravaDotNet;
using StravaDotNet.Components;
using StravaDotNet.Components.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AuthState>();
builder.Services.AddMudServices();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration["DBSettings:ConnectionString"]).LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddScoped<IStravaUserRepo, StravaUserRepo>();
builder.Services.AddScoped<IActivitiesRepo, ActivitiesRepo>();
builder.Services.AddScoped<IAthleteRepo, AthleteRepo>();
builder.Services.AddScoped<IMapRepo, MapRepo>();
builder.Services.AddScoped<ISegmentRepo, SegmentRepo>();
builder.Services.AddScoped<ISegmentEffortRepo, SegmentEffortRepo>();
builder.Services.AddScoped<IStreamSetRepo, StreamSetRepo>();
builder.Services.AddScoped<ITimeStreamRepo, TimeStreamRepo>();
builder.Services.AddScoped<IDistanceStreamRepo, DistanceStreamRepo>();
builder.Services.AddScoped<ILatLngStreamRepo, LatLngStreamRepo>();
builder.Services.AddScoped<ISmoothGradeStreamRepo, SmoothGradeStreamRepo>();
builder.Services.AddScoped<IMovingStreamRepo, MovingStreamRepo>();
builder.Services.AddScoped<ITemperatureStreamRepo, TemperatureStreamRepo>();
builder.Services.AddScoped<IPowerStreamRepo, PowerStreamRepo>();
builder.Services.AddScoped<ICadenceStreamRepo, CadenceStreamRepo>();
builder.Services.AddScoped<IHeartrateStreamRepo, HeartrateStreamRepo>();
builder.Services.AddScoped<ISmoothVelocityStreamRepo, SmoothVelocityStreamRepo>();
builder.Services.AddScoped<IAltitudeStreamRepo, AltitudeStreamRepo>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddHttpClient<DetailedActivityService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("AppSettings")["BaseAddress"]);
});
builder.Services.AddHttpClient<SegmentEffortService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("AppSettings")["BaseAddress"]);
});
builder.Services.AddScoped<HeatmapService>();
builder.Services.AddScoped<DataRetrievalService>();
builder.Services.AddScoped<PlottingHelperService>();
builder.Services.AddScoped<StatsService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<IStravaService, StravaService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Components/Pages");

var baseUrl = builder.Configuration["BaseAddress"];
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();
app.MapRazorPages(); // Ensure Razor Pages are mapped

app.Run();
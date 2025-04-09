using Data.Repos;
using StravaDotNet.Components;
using Microsoft.EntityFrameworkCore;
using Data.Interfaces;
using Data.Context;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StravaDotNet.Components.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMudServices();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Data")));

builder.Services.AddScoped<IStravaUserRepo, StravaUserRepo>();
builder.Services.AddScoped<IActivitiesRepo, ActivitiesRepo>();
builder.Services.AddScoped<IAthleteRepo, AthleteRepo>();
builder.Services.AddScoped<IMapRepo, MapRepo>();
builder.Services.AddScoped<ISegmentRepo, SegmentRepo>();
builder.Services.AddScoped<ISegmentEffortRepo, SegmentEffortRepo>();

builder.Services.AddScoped<SegmentEffortService, SegmentEffortService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.Configure<RazorPagesOptions>(options => options.RootDirectory = "/Components/Pages");

builder.Services.AddHttpClient<DetailedActivityService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseAddress"]);
});

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
//app.MapBlazorHub();
app.MapRazorPages(); // Ensure Razor Pages are mapped
//app.MapFallbackToPage("/_Host");

app.Run();

using Blazored.LocalStorage;

using Lab4.Data;
using Lab4.Data.ApplicationDbContext;
using Lab4.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BlogsDb");
// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
{
	options.UseSqlServer(connectionString);
});
builder.Services.AddBlazoredLocalStorage(options => 
{
	options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddTransient<IExceptionHandler, ExceptionHandler>();
builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IPostsService, PostsService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

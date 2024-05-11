using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDataAccessLayer.Repositories.Concretes;

namespace XploreFlix;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();
		var ConStr = new Microsoft.Extensions.Configuration.ConfigurationManager()
			.AddJsonFile("appsettings.json")
			.Build()
			.GetConnectionString("DefaultConnection");

		builder.Services.AddDbContext<XploreFlixDbContext>(options =>
						options.UseSqlServer(ConStr));


		//Added Services_____________________________
		builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
		builder.Services.AddScoped<IMovieInCinemaRepository, MovieInCinemaRepository>();
		builder.Services.AddScoped<IMovieRepository, MovieRepository>();
		builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
		builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
		builder.Services.AddScoped<IActorRepository, ActorRepository>();
		builder.Services.AddScoped<IMovieActorRepository, MovieActorRepository>();
		builder.Services.AddScoped<ICardRepository, CardRepository>();
		builder.Services.AddScoped<IMovieOrderRepository, MovieOrderRepository>();
		builder.Services.AddScoped<IUpdateProfileRepository, UpdateProfileRepository>();

		builder.Services.AddMemoryCache();
		builder.Services.AddSession();
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseSession();

		app.UseAuthentication();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}
}

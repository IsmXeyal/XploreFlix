using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using XploreFlix.Models;
using XploreFlix.Payment;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDataAccessLayer.Repositories.Concretes;
using XploreFlixDomainLayer.Entities;

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

		builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
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

        //___________________________________________
        //Authentication and Authorization

        builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<XploreFlixDbContext>();
        builder.Services.AddMemoryCache();
		builder.Services.AddSession();
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		});

		var app = builder.Build();

        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseExceptionHandler("/Home/Error");

        app.UseStaticFiles();
        app.UseRouting();
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

        // Database Data Initializer
        DbInitializer.SeedDB(app).Wait();
        DbInitializer.CreateUsersAndRolesAsync(app).Wait();

        app.Run();

    }
}

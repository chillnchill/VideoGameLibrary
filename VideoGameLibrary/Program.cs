using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.ModelBinders;
using VideoGameLibrary.Services.Data.Interfaces;
using static VideoGameLibrary.Common.GeneralApplicationConstants;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


string connectionString =
	builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<VideoGameLibraryDbContext>(options =>
	options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
	options.SignIn.RequireConfirmedAccount =
		builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
	options.Password.RequireLowercase =
		builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
	options.Password.RequireUppercase =
		builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
	options.Password.RequireNonAlphanumeric =
		builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
	options.Password.RequiredLength =
		builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
})
.AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<VideoGameLibraryDbContext>();


builder.Services.AddApplicationServices(typeof(IGameService));
builder.Services.AddScoped<UserManager<ApplicationUser>>();

builder.Services.AddRecaptchaService();

builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();

builder.Services.ConfigureApplicationCookie(cfg =>
{
	cfg.LoginPath = "/User/Login";
	cfg.AccessDeniedPath = "/Home/Error/401";
});

builder.Services.AddControllersWithViews()
	.AddMvcOptions(options =>
	{
		options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
		options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
	});

WebApplication app = builder.Build();


if (app.Environment.IsDevelopment())
{

	app.UseMigrationsEndPoint();
	app.UseDeveloperExceptionPage();

}
else
{
	app.UseExceptionHandler("/Home/Error/500");
	app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
	app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.EnableOnlineUsersCheck();

if (app.Environment.IsDevelopment())
{
	app.SeedAdministrator(DevelopmentAdminEmail);
}

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);

	app.MapDefaultControllerRoute();

	app.MapRazorPages();
});

app.Run();

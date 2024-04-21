namespace Microsoft.Extensions.DependencyInjection.Extensions
{
	using DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using System.Reflection;
    using VideoGameLibrary.Data.Models;
	using VideoGameLibrary.Web.Infrastructure.Middleware;
	using static VideoGameLibrary.Common.GeneralApplicationConstants;
    public static class WebApplicationBuilderExtensions
	{

		public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
		{
			Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
			if (serviceAssembly == null)
			{
				throw new InvalidOperationException("Invalid service type provided!");
			}

			Type[] implementationTypes = serviceAssembly
				.GetTypes()
				.Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
				.ToArray();

			foreach (Type implementationType in implementationTypes)
			{
				Type? interfaceType = implementationType
					.GetInterface($"I{implementationType.Name}");

				if (interfaceType == null)
				{
					throw new InvalidOperationException(
						$"No interface is provided for the service with name: {implementationType.Name}");
				}

				services.AddScoped(interfaceType, implementationType);
			}
		}

		public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
		{
			//this allows us to take all service providers we need because the inversion of control is not available for
			//static classes

			//sync section
			using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

			IServiceProvider serviceProvider = scopedServices.ServiceProvider;

			UserManager<ApplicationUser> userManager =
				serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			RoleManager<IdentityRole<Guid>> roleManager =
				serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

			//async section
			Task.Run(async () =>
			{
				if (await roleManager.RoleExistsAsync(AdminRoleName))
				{
					return;
				}

				IdentityRole<Guid> role =
					new IdentityRole<Guid>(AdminRoleName);

				await roleManager.CreateAsync(role);

				ApplicationUser adminUser =
					await userManager.FindByEmailAsync(email);

				await userManager.AddToRoleAsync(adminUser, AdminRoleName);
			})
			.GetAwaiter()
			.GetResult();

			return app;
		}

		public static IApplicationBuilder EnableOnlineUsersCheck(this IApplicationBuilder app)
		{
			return app.UseMiddleware<OnlineUsersMiddleware>();
		}
	}

}
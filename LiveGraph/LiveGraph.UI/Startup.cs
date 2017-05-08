using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LiveGraph.UI.Models;
using LiveGraph.UI.Services;
using LiveGraph.BLL;
using LiveGraph.Common;
using Microsoft.AspNetCore.Http;
using LiveGraph.InterfaceDao;
using LiveGraph.DAL;
using LiveGraph.InterfaceBLL;
using LiveGraph.Validation.Interface;
using LiveGraph.Validation;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using AutoMapper;
using Microsoft.Extensions.Options;
using LiveGraph.UI.Extension;
using Microsoft.AspNetCore.Authorization;

namespace LiveGraph.UI
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json")
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment())
			{
				// For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
				builder.AddUserSecrets<Startup>();
			}

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new[]
				{
						 new CultureInfo("en-US"),
							new CultureInfo("ru-RU")
				};

				options.DefaultRequestCulture = new RequestCulture(culture: "ru-RU", uiCulture: "ru-RU");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;


			});

			services.AddLocalization(options => options.ResourcesPath = "Resources");

			services.AddMvc()
			  .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
			  .AddDataAnnotationsLocalization();

			services.Configure<SettingsDao>(op =>
			{
				var nameString = Configuration.GetSection("NameConnectionStrings").Value.ToString();
				op.ConnectionString = Configuration.GetConnectionString(nameString);
			});

			services.AddClaims(Configuration);

			//	.AddAuthorization(options =>
			//{
			//	var claims = Configuration.GetSection("Claims").GetChildren(); 

			//	//foreach (var item in claims)
			//	//{
			//	//	options.AddPolicy()
			//	//}
			//	options.AddPolicy("VipPolicy", policy => policy.RequireClaim("isVip"));
			//});

			services.AddAutoMapper(typeof(Startup));

			services.AddTransient<IAppUserDao, AppUserDao>();
			services.AddTransient<IAppUserBLL, AppUserBLL>();
			services.AddTransient<IAppUserValidation, AppUserValidation>();
			services.AddTransient<IPageDao, PageDao>();
			services.AddTransient<IPageBLL, PageBLL>();
		
		}
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
			var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(locOptions.Value);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				app.UseBrowserLink();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();



			//app.UseIdentity();


			// Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715


			app.UseStaticFiles();

			app.UseCookieAuthentication(new CookieAuthenticationOptions()
			{
				AuthenticationScheme = "MyScheme",
				LoginPath = new PathString("/Account/Login/"),
				AccessDeniedPath = new PathString("/Account/Forbidden/"),
				AutomaticAuthenticate = true,
				AutomaticChallenge = true
			});



			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}

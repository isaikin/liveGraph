using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LiveGraph.UI.Models;
using LiveGraph.UI.Models.AccountViewModels;
using LiveGraph.UI.Services;
using LiveGraph.Common;
using LiveGraph.InterfaceBLL;
using LiveGraph.UI.Extension;

namespace LiveGraph.UI.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly IAppUserBLL _appUserLL;

		public AccountController(
			IAppUserBLL appUserLL
			)
		{
			_appUserLL = appUserLL;
		}

		//
		// GET: /Account/Login
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Login(string returnUrl = null)
		{
			await HttpContext.Authentication.SignOutAsync("MyScheme");

			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			
			if (ModelState.IsValid)
			{
				var idAppUser = _appUserLL.Login(model.Login, model.Password);
				if (idAppUser.HasValue)
				{
					var principal = _appUserLL.ClaimsPrincipalById(idAppUser.Value);
					await HttpContext.Authentication.SignInAsync("MyScheme", principal);
				}
				return RedirectToLocal(returnUrl);
			}
			

			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		//
		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
		{
			List<CustomError> errors;
			if (!_appUserLL.Registration(new AppUser {Login = model.Login, Email = model.Email, Password = model.Password.Select(x => (byte)x).ToList() }, out errors)
			)
			{
				ModelState.AddModelError(errors);

			}
			else
			{
				var idAppUser = _appUserLL.Login(model.Login, model.Password);
				if (idAppUser.HasValue)
				{
					var principal = _appUserLL.ClaimsPrincipalById(idAppUser.Value);
					await HttpContext.Authentication.SignInAsync("MyScheme", principal);
				}

				RedirectToLocal(returnUrl);
			}

			return View(model);
		}

		//
		// POST: /Account/Logout
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.Authentication.SignOutAsync("MyScheme");
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}

		
		#region Helpers

		private IActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction(nameof(HomeController.Index), "Home");
			}
		}

		#endregion
	}
}

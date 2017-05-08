using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using LiveGraph.InterfaceBLL;
using LiveGraph.UI.Models;
using AutoMapper;

namespace LiveGraph.UI.Controllers
{
    public class HomeController : Controller
    {
		private readonly IMapper _mapper;
		private readonly IPageBLL _pageBLL;

		public HomeController(IMapper mapper, IPageBLL pageBLL)
		{
			_mapper = mapper;
			_pageBLL = pageBLL;
		}
		public IActionResult Index()
        {
			var result = _mapper.Map<IEnumerable<Page>>(_pageBLL.GetAll()).ToList();
			return View(result);
		}

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

		[HttpPost]
		public IActionResult SetLanguage(string culture, string returnUrl)
		{

			Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
			);

			return LocalRedirect(returnUrl);
		}
	}
}

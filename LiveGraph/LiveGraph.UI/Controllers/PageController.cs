using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiveGraph.UI.Models;
using AutoMapper;
using LiveGraph.Common;
using LiveGraph.InterfaceBLL;
using Microsoft.AspNetCore.Authorization;

namespace LiveGraph.UI.Controllers
{
	[Authorize(Policy = "Admin")]
	public class PageController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IPageBLL _pageBLL;

		public PageController(IMapper mapper, IPageBLL pageBLL)
		{
			_mapper = mapper;
			_pageBLL = pageBLL;
		}

		[HttpGet]
				[AllowAnonymous]
		public IActionResult Index(int id)
		{
			var page = _mapper.Map<Page>(_pageBLL.GetById(id));

			return View(page);
		}

		[HttpPost]
		public IActionResult Index(Page page)
		{
			_pageBLL.Update(_mapper.Map<PageDto>(page));

			return RedirectToAction("Index", page.Id);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreatePage page)
		{
			var tempPage = _mapper.Map<PageDto>(page);

			_pageBLL.Add(tempPage);
			return RedirectToAction("Index","Home");
		}

		[AllowAnonymous]
		public IActionResult GetAll()
		{
			var result = _pageBLL.GetAll();
			return View(result);
		}
	}
}
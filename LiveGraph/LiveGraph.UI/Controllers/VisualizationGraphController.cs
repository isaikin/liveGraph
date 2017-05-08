using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LiveGraph.UI.Controllers
{
    public class VisualizationGraphController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
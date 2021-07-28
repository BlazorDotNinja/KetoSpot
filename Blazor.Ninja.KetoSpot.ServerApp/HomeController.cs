using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Blazor.Ninja.KetoSpot.ServerApp
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebBruteforce.Models;

namespace WebBruteforce.Controllers
{
	public class HomeController : Controller
	{
		public ViewResult Index()
		{
			return View();
		}

		public ViewResult BruteforceResult()
		{
			return View();
		}

	}
}
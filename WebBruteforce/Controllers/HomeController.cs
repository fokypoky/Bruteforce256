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

		[HttpPost]
		public ViewResult BruteforceResult(HashInput hashInput)
		{
			var output = new HashOutput()
			{
				Results = new List<string>() { "123: 456\n", "12345: 543\n", "32: 11\n" },
				Errors = new List<string>() { "YOU ARE IDIOT"}
			};
			return View(output);
		}

	}
}
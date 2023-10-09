using Microsoft.AspNetCore.Mvc;
using BruteforceLib.Bruteforce.Implementation;
using BruteforceLib.Hashers.Implementation;
using WebBruteforce.Infrastructure.Repositories.Implementation;
using WebBruteforce.Models;
using System.IO;

namespace WebBruteforce.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ViewResult BruteforceResult(HashInput hashInput)
		{
			var hashOutput = new HashOutput()
			{
				Errors = new List<string>(),
				Results = new List<string>()
			};

			if (hashInput.ThreadsCount is null)
			{
				hashOutput.Errors.Add("No threads count");
				return View(hashOutput);
			}

			if (String.IsNullOrWhiteSpace(hashInput.Hashes) && String.IsNullOrWhiteSpace(hashInput.FilePath))
			{
				hashOutput.Errors.Add("No hashes specified\n");
				return View(hashOutput);
			}

			var hashes = new List<string>();

			if (!String.IsNullOrWhiteSpace(hashInput.FilePath))
			{
				if (System.IO.File.Exists(hashInput.FilePath))
				{
					var repository = new TextFileRepository();
					foreach (var hash in repository.ReadFile(hashInput.FilePath).Split("\n"))
					{
						hashes.Add(hash);
					}
				}
				else
				{
					hashOutput.Errors.Add("File not exists");
					return View(hashOutput);
				}
				
			}
			else
			{
				foreach (var hash in hashInput.Hashes.Split("\r\n"))
				{
					hashes.Add(hash.Replace(" ", ""));
				}
			}

			var bruteforce = new Bruteforcer(hashes, new Sha256Hasher(), (int)hashInput.ThreadsCount);
			bruteforce.Bruteforce();

			if (bruteforce.Results.Count == 0)
			{
				hashOutput.Results.Add("Not results");
			}

			foreach (var hash in bruteforce.Results)
			{
				hashOutput.Results.Add(hash + "\n");
			}

			return View(hashOutput);
		}

	}
}
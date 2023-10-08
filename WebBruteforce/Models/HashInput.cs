using System.ComponentModel.DataAnnotations;

namespace WebBruteforce.Models
{
	public class HashInput
	{
		public string? Hashes { get; set; }
		public string? FilePath { get; set; }
		[Required(ErrorMessage = "Please specify threads count")]
		public int? ThreadsCount { get; set; }
	}
}

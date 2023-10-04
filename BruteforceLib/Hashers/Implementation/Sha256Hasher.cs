using System.Security.Cryptography;
using System.Text;
using BruteforceLib.Hashers.Interfaces;

namespace BruteforceLib.Hashers.Implementation
{
	public class Sha256Hasher : IHasher
	{
		public string Hash(string text)
		{
			using (var sha256 = SHA256.Create())
			{
				var hashBytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(text));
				
				var resultBuilder = new StringBuilder();

				foreach (var hashByte in hashBytes)
				{
					resultBuilder.Append(hashByte.ToString("x2"));
				}

				return resultBuilder.ToString();
			}
		}
	}
}

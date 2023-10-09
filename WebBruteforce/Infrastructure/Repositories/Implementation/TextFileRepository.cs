using System.Text;
using WebBruteforce.Infrastructure.Repositories.Interfaces;

namespace WebBruteforce.Infrastructure.Repositories.Implementation
{
	public class TextFileRepository: IFileRepository<string>
	{
		public void WriteFile(string writeObject, string filePath)
		{
			throw new NotImplementedException();
		}

		public string? ReadFile(string filePath)
		{
			var resultBuilder = new StringBuilder();
			using (var reader = new StreamReader(filePath))
			{
				while (!reader.EndOfStream)
				{
					resultBuilder.Append(reader.ReadLine() + "\n");
				}
			}

			return resultBuilder.ToString();
		}
	}
}

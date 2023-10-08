using BruteforceLib.Bruteforce.Implementation;
using BruteforceLib.Hashers.Implementation;

namespace ConsoleTest
{
	public class Program
	{
		public static List<string> Hashes
		{
			get => new List<string>()
			{
				"1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad", // zyzzx
				"3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b", // apple
				"74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f" // mmmmm
			};
		}

		public static void Main()
		{
			var bruteforce = new Bruteforcer(Hashes, new Sha256Hasher(), 100);
			
			bruteforce.Bruteforce();

			foreach (var result in bruteforce.Results)
			{
				Console.WriteLine(result);
			}
		}

		static string GetWordFromPosition(int position)
		{
			const int alphabetSize = 26;
			if (position < 1 || position > Math.Pow(alphabetSize, 5))
			{
				throw new ArgumentException("Недопустимая позиция");
			}

			char[] word = new char[5];
			position--; // Уменьшаем позицию на 1, чтобы начать с 0

			for (int i = 4; i >= 0; i--)
			{
				int remainder = position % alphabetSize;
				word[i] = (char)('a' + remainder);
				position /= alphabetSize;
			}

			return new string(word);
		}
	}
}
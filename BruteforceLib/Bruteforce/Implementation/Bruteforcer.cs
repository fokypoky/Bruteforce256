using System.Diagnostics;
using BruteforceLib.Bruteforce.Interfaces;
using BruteforceLib.Hashers.Interfaces;

namespace BruteforceLib.Bruteforce.Implementation
{
	public class Bruteforcer : IBruteforcer
	{
		public List<string> Results { get; set; }
		public IEnumerable<string> Hashes { get; set; }
		public IHasher Hasher { get; set; }
		public int ThreadsCount { get; set; }

		/// <summary>
		///	Количество возможных слов из 5 букв 
		/// </summary>
		private const int WordsCount = 11881376;

		public Bruteforcer(IEnumerable<string> hashes, IHasher hasher, int threadsCount)
		{
			Results = new List<string>();
			Hashes = hashes;
			Hasher = hasher;
			ThreadsCount = threadsCount;
		}

		public void Bruteforce()
		{
			Debug.WriteLine("START");

			var indexes = new List<int>();
			int wordsPerThreadCount = WordsCount / ThreadsCount;

			// выделяю количество слов для каждого потока
			for (int i = 1; i <= ThreadsCount; i++)
			{
				if (i == ThreadsCount)
				{
					indexes.Add(11881376);
					break;
				}
				indexes.Add(i * wordsPerThreadCount);
			}

			var threads = new List<Thread>();

			// создаю и запускаю потоки
			foreach (var index in indexes)
			{
				var forceObject = new ForceObject();

				if (indexes.IndexOf(index) == 0)
				{
					forceObject.StartIndex = 1;
					forceObject.EndIndex = index;
				}
				else
				{
					forceObject.StartIndex = indexes[indexes.IndexOf(index) - 1] + 1;
					forceObject.EndIndex = index;
				}

				var thread = new Thread(Force);
				thread.Start(forceObject);

				threads.Add(thread);
			}

			// ожидаю пока не завершатся
			foreach (var thread in threads)
			{
				thread.Join();
			}

		}

		private void Force(object parameter)
		{
			if (parameter is null || parameter is not ForceObject)
			{
				throw new ArgumentException("ForceObject expected");
			}

			var forceObject = (ForceObject)parameter;

			for (int i = forceObject.StartIndex; i <= forceObject.EndIndex; i++)
			{
				string currentWord = GetCombinationByPosition(i);
				string currentWordHash = Hasher.Hash(currentWord);
				if (Hashes.Contains(currentWordHash))
				{
					lock (Results)
					{
						Results.Add($"{currentWordHash} = {currentWord}");
					}
				}
			}

		}

		private string GetCombinationByPosition(int position)
		{ 
			int alphabetSize = 26;
			
			var word = new char[5];

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

using BruteforceLib.Hashers.Interfaces;

namespace BruteforceLib.Bruteforce.Interfaces
{
	public interface IBruteforcer
	{
		List<string> Results { get; set; }
		IEnumerable<string> Hashes { get; set; }
		IHasher Hasher { get; set; }
		void Bruteforce();
	}
}

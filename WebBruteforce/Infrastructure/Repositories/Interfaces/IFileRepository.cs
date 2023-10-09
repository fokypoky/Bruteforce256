namespace WebBruteforce.Infrastructure.Repositories.Interfaces
{
	public interface IFileRepository<T>
	{
		void WriteFile(T writeObject, string filePath);
		T? ReadFile(string filePath);
	}
}

namespace Core
{
	public interface IUpdatable
	{
		DisposeHandle handle {set;}

		void PerformUpdate();
		void Dispose();
	}
}

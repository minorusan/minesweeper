using Core;

public class DisposeHandle
{
	public enum EDisposeHandleState {Active, Disposed};
	public IUpdatable _myUpdatable;
	public IUpdatable updatable
	{
		get
		{
			return _myUpdatable;
		}
	}

	public DisposeHandle (IUpdatable my)
	{
		_myUpdatable = my;
	}

	public EDisposeHandleState state
	{
		get;
		private set;
	}

	public void Dispose()
	{
		state = EDisposeHandleState.Disposed;
	}
}

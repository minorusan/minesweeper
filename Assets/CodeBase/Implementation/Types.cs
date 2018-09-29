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

public enum EGridState {Revealed, Closed, Flagged}

public struct GridPosition
{
	public readonly int x;
	public readonly int y;

	public GridPosition (int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}

public struct GridNode
{
	public EGridState currentState;
	public readonly GridPosition position;
	public bool containsBomb;

	public GridNode (GridPosition position)
	{
		this.position = position;
		this.currentState = EGridState.Closed;
		this.containsBomb = false;
	}
}

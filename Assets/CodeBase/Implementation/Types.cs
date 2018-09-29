using Core;
using System;

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

public enum EGridNodeState {Revealed, Closed, Flagged}

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

public enum EGameResult {Win, Lost};

public class GridNode
{
	public EGridNodeState currentState;
	public event Action<EGridNodeState> onStateChanged = delegate{};
	public readonly GridPosition position;

	public int adjacentBombs;
	public bool requiresDelayForDrawing;
	public bool containsBomb;

	public void ChangeState(EGridNodeState state)
	{
		if (state != currentState)
		{
			currentState = state;
			onStateChanged(currentState);
		}
	}

	public GridNode (GridPosition position)
	{
		this.position = position;
		this.currentState = EGridNodeState.Closed;
		this.containsBomb = false;
	}
}

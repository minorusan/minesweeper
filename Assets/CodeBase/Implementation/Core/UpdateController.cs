using System.Collections;
using System.Collections.Generic;


namespace Core
{
	public class UpdateController 
	{
		private const int AVARAGE_UPDATERS_COUNT = 1000;
		private List<IUpdatable> _updateQueue = new List<IUpdatable>(AVARAGE_UPDATERS_COUNT);
		private List<DisposeHandle> _disposeHandles = new List<DisposeHandle>();

		public void AddUpdatable(IUpdatable newUpdatable)
		{
			var newHandle = new DisposeHandle(newUpdatable);
			newUpdatable.handle = newHandle;

			_disposeHandles.Add(newHandle);
			_updateQueue.Add(newUpdatable);
		}

		public void Update()
		{
			var count = _updateQueue.Count;
			for (int i = 0; i < count; i++)
			{
				_updateQueue[i].PerformUpdate();
			}

			count = _disposeHandles.Count;
			for (int i = 0; i < count; i++)
			{
				var handle = _disposeHandles[i];
				if (handle.state == DisposeHandle.EDisposeHandleState.Disposed)
				{
					_updateQueue.Remove(handle.updatable);
					handle.updatable.Dispose();
					_disposeHandles.Remove(handle);
				}
			}
		}
	}
}
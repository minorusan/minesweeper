using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
	public class CommandExecutor : IUpdatable
	{
		private const int AVARAGE_COMMANDS_PER_FRAME_COUNT = 1000;
		private Queue<ICommand> _comandsQueue = new Queue<ICommand>(AVARAGE_COMMANDS_PER_FRAME_COUNT);

		public void EnqueueCommand(ICommand command)
		{
			_comandsQueue.Enqueue(command);
		}
			
		#region IUpdatable implementation
		void IUpdatable.PerformUpdate()
		{
			if (_comandsQueue.Count > 0)
			{
				_comandsQueue.Dequeue().Execute();
			}
		}

		void IUpdatable.Dispose()
		{
			_comandsQueue.Clear();
		}

		DisposeHandle IUpdatable.handle
		{
			set
			{
			}
		}
		#endregion
	}
}
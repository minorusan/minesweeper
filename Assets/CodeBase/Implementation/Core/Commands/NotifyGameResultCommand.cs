using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Commands
{
	public class NotifyGameResultCommand : ICommand
	{
		private EGameResult _gameResult;

		public NotifyGameResultCommand (EGameResult result)
		{
			_gameResult = result;
		}

		#region ICommand implementation
		public void Execute()
		{
			Debug.Log(_gameResult.ToString().ToUpper());
		}
		#endregion
		
	}
}


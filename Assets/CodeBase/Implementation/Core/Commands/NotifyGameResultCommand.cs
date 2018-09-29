using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Behaviours;

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
			GameObject.FindObjectOfType<GameSessionBehaviour>().ChangeGameState(_gameResult);
		}
		#endregion
		
	}
}


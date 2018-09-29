using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Commands
{
	public class CompositeCommand : ICommand
	{
		private List<ICommand> _commands;

		public CompositeCommand (List<ICommand> commands)
		{
			_commands = commands;
		}

		#region ICommand implementation
		public void Execute()
		{
			foreach (var item in _commands)
			{
				item.Execute();
			}
		}
		#endregion
	}
}


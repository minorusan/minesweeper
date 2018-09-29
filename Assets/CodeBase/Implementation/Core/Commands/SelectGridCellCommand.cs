using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Commands
{
	public class SelectGridCellCommand : ICommand
	{
		private GridNode _node;

		public SelectGridCellCommand (GridNode node)
		{
			_node = node;
		}

		#region ICommand implementation
		public void Execute()
		{
			ServiceProvider.GetService<GridController>().PerformSelectionOnNode(_node);
		}
		#endregion
		
	}
}


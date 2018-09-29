using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Behaviours;

namespace Core.Commands
{
	public class DestroySingleBombCommand : ICommand
	{
		private GridNode _node;

		public DestroySingleBombCommand (GridNode node)
		{
			_node = node;
		}

		#region ICommand implementation

		public void Execute()
		{
			GameObject.FindObjectOfType<GridCreationBehaviour>().DemolishSingleBomb(_node);
		}

		#endregion
		
	}
}


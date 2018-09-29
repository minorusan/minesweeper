using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Behaviours;

namespace Core.Commands
{
	public class DestroyGridCommand : ICommand
	{
		#region ICommand implementation

		public void Execute()
		{
			GameObject.FindObjectOfType<GridCreationBehaviour>().DemolishBombs();
		}

		#endregion
	}
}


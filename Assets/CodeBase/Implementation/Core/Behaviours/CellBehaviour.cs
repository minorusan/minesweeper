using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Commands;

namespace Core.Behaviours
{
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(MeshRenderer))]
	public class CellBehaviour : MonoBehaviour 
	{
		private GridNode _node;
		private MeshRenderer _renderer;

		public CellSettings settings; 


		#region MonoBehaviour

		private void Awake()
		{
			_renderer = GetComponent<MeshRenderer>();
			_renderer.sharedMaterial = settings.closedMaterial;
		}

		public void InitWithNode(GridNode node)
		{
			_node = node;
			_node.onStateChanged += OnNodeStateChanged;
		}

		#endregion

		void OnNodeStateChanged (EGridNodeState obj)
		{
			switch (obj)
			{
				case EGridNodeState.Revealed:
					{
						ChangeMaterialToRevealed();
						break;
					}
				default:
					_renderer.sharedMaterial = settings.closedMaterial;
					break;
			}
		}

		private void ChangeMaterialToRevealed()
		{
			var adjacentBombs = _node.adjacentBombs;
			Material material;
			switch (adjacentBombs)
			{
				case 0:
					{
						material = settings.revealedMaterial;
						break;
					}
				case 1:
					{
						material = settings.blueMaterial;
						break;
					}
				case 2:
					{
						material = settings.greenMaterial;
						break;
					}
				default:
					material = settings.redMaterial;
					break;
			}
			_renderer.sharedMaterial = material;
		}

		public void PerformClick()
		{
			ServiceProvider.GetService<CommandExecutor>().EnqueueCommand(new SelectGridCellCommand(_node));
		}
	}
}
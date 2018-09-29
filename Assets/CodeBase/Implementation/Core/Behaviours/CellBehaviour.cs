using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Commands;

namespace Core.Behaviours
{
	[RequireComponent(typeof(MeshRenderer))]
	public partial class CellBehaviour : MonoBehaviour 
	{
		private GridNode _node;
		private MeshRenderer _renderer;
	
		public CellSettings settings; 

		#region MonoBehaviour

		private void Awake()
		{
			_renderer = GetComponent<MeshRenderer>();
			_renderer.sharedMaterial = settings.closedMaterial;
			transform.localScale = Vector3.zero;
		}

		public void InitWithNode(GridNode node)
		{
			_node = node;
			_node.onStateChanged += OnNodeStateChanged;
		}

		private void OnEnable()
		{
			Invoke("EnableAnimatorDelayed", Random.Range(0f, settings.maxAppearDelay));
		}

		private void EnableAnimatorDelayed()
		{
			GetComponent<Animator>().enabled = true;
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
	
		public void PerformClick()
		{
			ServiceProvider.GetService<CommandExecutor>().EnqueueCommand(new SelectGridCellCommand(_node));
		}
	}
}
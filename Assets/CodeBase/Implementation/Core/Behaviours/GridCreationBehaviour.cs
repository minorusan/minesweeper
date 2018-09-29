using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Settings;

namespace Core.Behaviours
{
	public class GridCreationBehaviour : MonoBehaviour 
	{
		private GridController _controller;

		public GridSettings gridCreationSettings;
		public GridCreationSettings gridDisplaySettings;

		private List<CellBehaviour> _cells;
		private List<CellBehaviour> _bombs;
		private Dictionary<GridNode, CellBehaviour> _nodesToCellsMap;

		#region MonoBehaviour
		private void Awake()
		{
			_controller = ServiceProvider.GetService<GridController>();
			_controller.onGridInitialized += OnNewGridInitialized;
			_bombs = new List<CellBehaviour>(gridCreationSettings.bombsPlacer.maxBombsCount);
			_cells = new List<CellBehaviour>(gridCreationSettings.height * gridCreationSettings.width);
			_nodesToCellsMap = new Dictionary<GridNode, CellBehaviour>(_cells.Count);
			_controller.InitializeWithSettings(gridCreationSettings);
		}

		void OnNewGridInitialized (GridNode[,] obj)
		{
			float previousX = 0f;
			for (int i = 0; i < gridCreationSettings.width; i++)
			{
				float previousY = 0f;
				for (int j = 0; j < gridCreationSettings.height; j++) 
				{
					var newGridCell = Instantiate(gridDisplaySettings.gridNodePrefab, transform);
					var localPosition = new Vector2(previousX,(previousY + gridDisplaySettings.gridCellDimention) + gridDisplaySettings.gridPadding);
					previousY = localPosition.y;

					newGridCell.transform.localPosition = localPosition;
					InitializeCell(newGridCell, obj[i,j]);
				}
				previousX = previousX + gridDisplaySettings.gridCellDimention + gridDisplaySettings.gridPadding;
			}
		}

		private void InitializeCell(GameObject newCell, GridNode node)
		{
			var cellBehaviour = newCell.GetComponent<CellBehaviour>();

			if (node.containsBomb)
			{
				_bombs.Add(cellBehaviour);
			}
			_nodesToCellsMap.Add(node, cellBehaviour);
			_cells.Add(cellBehaviour);
			cellBehaviour.InitWithNode(node);
		}
			
		#endregion

		public void DemolishSingleBomb(GridNode node)
		{
			var cellBehaviour = _nodesToCellsMap[node];

			_bombs.Remove(cellBehaviour);
			_cells.Remove(cellBehaviour);
			_nodesToCellsMap.Remove(node);
			cellBehaviour.Explode();
		}

		public void DemolishBombs()
		{
			foreach (var item in _cells)
			{
				var rb = item.GetComponent<Rigidbody>();
				rb.isKinematic = false;
			}
			StartCoroutine(DemolishBombsRoutine());
		}

		private IEnumerator DemolishBombsRoutine()
		{
			foreach (var item in _bombs)
			{
				yield return new WaitForSeconds(Random.Range(0f, gridDisplaySettings.maxDemolitionDelay));
				item.Explode();
			}
		}
	}
}
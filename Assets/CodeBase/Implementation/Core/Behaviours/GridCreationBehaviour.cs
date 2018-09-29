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

		#region MonoBehaviour
		private void Awake()
		{
			_controller = ServiceProvider.GetService<GridController>();
			_controller.onGridInitialized += OnNewGridInitialized;
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
			cellBehaviour.InitWithNode(node);
		}
			
		#endregion

	}
}
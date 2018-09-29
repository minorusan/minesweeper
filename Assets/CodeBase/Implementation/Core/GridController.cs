using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Settings;
using System;

namespace Core
{
	public class GridController 
	{
		public event Action<GridNode[,]> onGridInitialized = delegate {};

		private GridSettings _settings;
		private GridNode[,] _nodesMatrix;

		public void InitializeWithSettings(GridSettings settings)
		{
			_settings = settings;
			InitializeGrid();
		}

		private void InitializeGrid()
		{
			_nodesMatrix = new GridNode[_settings.width, _settings.height];
			for (int i = 0; i < _settings.width; i++)
			{
				for (int j = 0; j < _settings.height; j++) {
					_nodesMatrix[i,j] = new GridNode(new GridPosition(i,j));
				}
			}

			_settings.bombsPlacer.PlaceBombs(ref _nodesMatrix);
			onGridInitialized(_nodesMatrix);
		}
	}
}


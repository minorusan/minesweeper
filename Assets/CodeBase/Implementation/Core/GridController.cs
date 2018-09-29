using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Core.Settings;
using Core.Commands;


namespace Core
{
	public class GridController 
	{
		public event Action<GridNode[,]> onGridInitialized = delegate {};

		private GridSettings _settings;
		private GridNode[,] _nodesMatrix;
		private List<GridNode> _uncheckedNodes;

		public void InitializeWithSettings(GridSettings settings)
		{
			_settings = settings;
			InitializeGrid();
		}

		private void InitializeGrid()
		{
			_uncheckedNodes = new List<GridNode>(_settings.width * _settings.height);
			_nodesMatrix = new GridNode[_settings.width, _settings.height];
			for (int i = 0; i < _settings.width; i++)
			{
				for (int j = 0; j < _settings.height; j++) {
					_nodesMatrix[i,j] = new GridNode(new GridPosition(i,j));
					_uncheckedNodes.Add(_nodesMatrix[i,j]);
				}
			}

			_settings.bombsPlacer.PlaceBombs(ref _nodesMatrix);

			onGridInitialized(_nodesMatrix);
		}
			
		public void PerformSelectionOnNode(GridNode node)
		{
			if (node.currentState == EGridNodeState.Closed)
			{
				if (node.containsBomb)
				{
					ServiceProvider.GetService<CommandExecutor>().EnqueueCommand(new NotifyGameResultCommand(EGameResult.Lost));
				}
				else
				{
					RevealNodesRecursive(node);
					CheckWinCondition();
				}	
			}
		}

		private void RevealNodesRecursive(GridNode node)
		{
			if (node.currentState == EGridNodeState.Revealed || node.currentState == EGridNodeState.Flagged)
			{
				return;
			}
				
			var ij = node.position;

			int minX = Math.Max(ij.x - 1, _nodesMatrix.GetLowerBound(0));
			int maxX = Math.Min(ij.x + 1, _nodesMatrix.GetUpperBound(0));
			int minY = Math.Max(ij.y - 1, _nodesMatrix.GetLowerBound(1));
			int maxY = Math.Min(ij.y + 1, _nodesMatrix.GetUpperBound(1));

			List<GridNode> candidates = new List<GridNode>();
			bool shouldCheckCandidates = true;
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					var candidate = _nodesMatrix[x,y];
					if (candidate.containsBomb)
					{
						node.adjacentBombs++;
						shouldCheckCandidates = false;
					}
					else
					{
						candidates.Add(candidate);
					}
				}
			}
			node.ChangeState(EGridNodeState.Revealed);
			_uncheckedNodes.Remove(node);

			if (shouldCheckCandidates)
			{
				foreach (var candidate in candidates)
				{
					RevealNodesRecursive(candidate);
				}
			}
		}

		private void CheckWinCondition()
		{
			bool winConditionFailed = _uncheckedNodes.Where(node=>!node.containsBomb).Count() > 0; 
			if (winConditionFailed == false)
			{
				ServiceProvider.GetService<CommandExecutor>().EnqueueCommand(new NotifyGameResultCommand(EGameResult.Win));
			}
		}
	}
}


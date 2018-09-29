using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.ScriptableAssets
{
	[CreateAssetMenu]
	public class SimpleRandomBombPlacer : BombsPlacer 
	{
		#region implemented abstract members of BombsPlacer

		public override void PlaceBombs(ref GridNode[,] matrix)
		{
			int placedBombs = 0;
			while (placedBombs < maxBombsCount)
			{
				var randomLower = Random.Range(0, matrix.GetUpperBound(0));
				var randomUpper = Random.Range(0, matrix.GetUpperBound(0));
				var node = matrix[randomLower, randomUpper];
				node.containsBomb = true;
				placedBombs++;
			}
		}

		#endregion

	}
}


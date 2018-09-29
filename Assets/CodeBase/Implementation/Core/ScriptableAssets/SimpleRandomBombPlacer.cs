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
			while (placedBombs <= maxBombsCount)
			{
				for (int i = 0; i < matrix.GetLength(0); i++)
				{
					for (int j = 0; j < matrix.GetLength(1); j++) {
						matrix[i,j].containsBomb = Random.value < 0.5f;
						if (matrix[i,j].containsBomb)
						{
							placedBombs++;
						}
					}
				}
			}
		}

		#endregion



	}
}


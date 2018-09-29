using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.ScriptableAssets
{
	public abstract class BombsPlacer : ScriptableObject
	{
		public int maxBombsCount;

		public abstract void PlaceBombs(ref GridNode[,] matrix);
	}
}
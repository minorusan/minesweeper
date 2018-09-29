using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.ScriptableAssets;

namespace Core.Settings
{
	[CreateAssetMenu]
	public class GridSettings:ScriptableObject 
	{
		public int width;
		public int height;

		public BombsPlacer bombsPlacer;
	}
}

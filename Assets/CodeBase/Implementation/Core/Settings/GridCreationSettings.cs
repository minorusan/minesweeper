using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Settings
{
	[CreateAssetMenu]
	public class GridCreationSettings : ScriptableObject 
	{
		public GameObject gridNodePrefab;

		public float maxDemolitionDelay = 1f;
		public float gridCellDimention = 1f;
		public float gridPadding = 0.5f;
	}
}
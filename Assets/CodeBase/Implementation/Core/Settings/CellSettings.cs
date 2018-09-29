using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Behaviours
{
	[CreateAssetMenu]
	public class CellSettings : ScriptableObject
	{
		public Material closedMaterial, revealedMaterial, redMaterial, blueMaterial, greenMaterial;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Behaviours
{
	[CreateAssetMenu]
	public class CellSettings : ScriptableObject
	{
		[Range(0f, 1f)]
		public float maxAppearDelay = 1f;
		[Range(0f, 1f)]
		public float materialLerpTime = 1f;
		public float explosionRadius;
		public float explosionPower;

		public GameObject demolitionParticles;
		public Material closedMaterial, revealedMaterial, redMaterial, blueMaterial, greenMaterial;

		[Range(0f, 1f)]
		public float multipleCellsAudioChange;
		public AudioClip[] clips;
	}
}
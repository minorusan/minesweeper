using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Commands;

namespace Core.Behaviours
{
	public partial class CellBehaviour : MonoBehaviour 
	{
		private float _zScale;
		private float _lerpedTime = 0f;

		#region MonoBehaviour

		private void Start()
		{
			if (Random.value < settings.multipleCellsAudioChange)
			{
				GetComponent<AudioSource>().clip = settings.clips[Random.Range(0, settings.clips.Length)];
				GetComponent<AudioSource>().Play();
			}
		}

		#endregion


		private void ChangeMaterialToRevealed()
		{ 
			var adjacentBombs = _node.adjacentBombs;
			Material material;
			switch (adjacentBombs)
			{
				case 0:
					{
						material = settings.revealedMaterial;
						_zScale = Random.Range(2f, 3f);
						break;
					}
				case 1:
					{
						material = settings.blueMaterial;
						_zScale = Random.Range(3f, 4f);
						break;
					}
				case 2:
					{
						material = settings.greenMaterial;
						_zScale = Random.Range(4f, 5f);
						break;
					}
				default:
					material = settings.redMaterial;
					_zScale = Random.Range(5f, 6f);
					break;
			}
			StartCoroutine(ChangeMaterialDelayed(material, _node.requiresDelayForDrawing));
		}

		private IEnumerator ChangeMaterialDelayed(Material mat, bool requiresDelay)
		{
			if (requiresDelay)
			{
				yield return new WaitForSeconds(Random.Range(0f, settings.maxAppearDelay));
				if (Random.value < settings.multipleCellsAudioChange)
				{
					ServiceProvider.GetService<CommandExecutor>().EnqueueCommand(
						new PlaySoundCommand(settings.clips[Random.Range(1, settings.clips.Length)], transform.position));
				}
			}
			else
			{
				ServiceProvider.GetService<CommandExecutor>().EnqueueCommand(
					new PlaySoundCommand(settings.clips[0], transform.position));
			}

			GetComponent<Animator>().enabled = false;
			var lerperdPercentage = 0f;
			var targetScale = new Vector3(1f, 1f, _zScale);
			while(lerperdPercentage <= 1f)
			{
				_lerpedTime += Time.deltaTime;
				lerperdPercentage = _lerpedTime / settings.materialLerpTime;
				_renderer.material.Lerp(settings.closedMaterial, mat, lerperdPercentage);
				transform.localScale = Vector3.Lerp(Vector3.one, targetScale, lerperdPercentage);
				yield return new WaitForEndOfFrame();
			}
		}

		public void Explode()
		{
			var nearby = Physics.OverlapSphere(transform.position, settings.explosionRadius);
			Instantiate(settings.demolitionParticles, transform);
			Destroy(GetComponent<Rigidbody>());

			_renderer.enabled = false;

			foreach (var cell in nearby)
			{
				var rb = cell.GetComponent<Rigidbody>();
				if (rb != null)
				{
					rb.isKinematic = false;
					rb.AddExplosionForce(settings.explosionPower, transform.position, settings.explosionRadius);
				}
			}
		}
	}
}
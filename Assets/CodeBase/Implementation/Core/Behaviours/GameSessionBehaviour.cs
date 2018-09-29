using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Commands;
using UnityEngine.SceneManagement;

namespace Core.Behaviours
{
	public class GameSessionBehaviour : MonoBehaviour 
	{
		private UpdateController _updater;
		public GameObject gameOverCanvas;
		public GameObject winCanvas;

		#region MonoBehaviour

		private void Awake()
		{
			_updater = ServiceProvider.GetService<UpdateController>();
			_updater.AddUpdatable(ServiceProvider.GetService<CommandExecutor>());
		}

		private void Update()
		{
			_updater.Update();
		}

		public void ChangeGameState(EGameResult result)
		{
			switch (result)
			{
				case EGameResult.Lost:
					{
						ServiceProvider.GetService<CommandExecutor>().EnqueueCommand(new DestroyGridCommand());
						gameOverCanvas.SetActive(true);
						break;
					}
				case EGameResult.Win:
					{
						winCanvas.SetActive(true);
						break;
					}
				default:
					break;
			}	
		}

		private void OnDestroy()
		{
			_updater.Dispose();
		}

		#endregion

		public void Quit()
		{
			Application.Quit();
		}

		public void ReloadLevel()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
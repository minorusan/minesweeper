using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Behaviours
{
	public class GameSessionBehaviour : MonoBehaviour 
	{
		private UpdateController _updater;

		#region MonoBehaviour

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			_updater = ServiceProvider.GetService<UpdateController>();

			_updater.AddUpdatable(ServiceProvider.GetService<CommandExecutor>());
		}

		private void Update()
		{
			_updater.Update();
		}

		#endregion
	}
}
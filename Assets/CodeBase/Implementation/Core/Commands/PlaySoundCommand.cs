using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Behaviours;

namespace Core.Commands
{
	public class PlaySoundCommand : ICommand
	{
		private AudioClip _clip;
		private Vector3 _position;

		public PlaySoundCommand (AudioClip sound, Vector3 position)
		{
			_clip = sound;
			_position = position;
		}

		#region ICommand implementation

		public void Execute()
		{
			AudioSource.PlayClipAtPoint(_clip, _position);
		}

		#endregion
	}
}


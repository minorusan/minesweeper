using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Behaviours
{
	[RequireComponent(typeof(Camera))]
	public class CameraInputBehaviour : MonoBehaviour 
	{
		private Camera _camera;

		#region MonoBehaviour

		private void Awake () 
		{
			_camera = GetComponent<Camera>();
		}

		private void Update ()
		{
			CheckInputIfNeeded();
		}

		#endregion

		private void CheckInputIfNeeded()
		{
			bool inputExists = Input.GetMouseButtonDown(0);
			if (inputExists)
			{
				RaycastHit hit;
				Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out hit)) 
				{
					CellBehaviour objectHit = hit.transform.gameObject.GetComponent<CellBehaviour>();
					if (objectHit != null)
					{
						objectHit.PerformClick();
					}
				}
			}
		}
	}
}
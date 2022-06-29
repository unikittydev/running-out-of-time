using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCameraBase _baseCamera;

	private CinemachineVirtualCameraBase _currentCamera;

	private void Awake()
	{
		_currentCamera = _baseCamera;
	}

	public void SwitchToCamera(CinemachineVirtualCameraBase camera)
	{
		if (camera == _baseCamera) 
		{
			ReturnToBaseCamera();
			return;
		}
		_currentCamera = camera;
		_currentCamera.Priority = 12;
	}

	public void ReturnToBaseCamera()
	{
		if (_currentCamera == _baseCamera) return;
		_currentCamera.Priority = 10;
		_currentCamera = _baseCamera;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lift : MonoBehaviour
{
	public UnityEvent OnOpen;
	public UnityEvent OnClose;

	[SerializeField] private Transform _leftDoor;
	[SerializeField] private Transform _rightDoor;
	[Space]
	[SerializeField] private Vector3 _leftDoorClosedPosition;
	[SerializeField] private Vector3 _leftDoorOpenedPosition;
	[SerializeField] private Vector3 _rightDoorClosedPosition;
	[SerializeField] private Vector3 _rightDoorOpenedPosition;
	[Space]
	[SerializeField] private float _doorMoveTime = 1f;

	public void Open()
	{
		StartCoroutine(Opening());
	}
	public void Close()
	{
		StartCoroutine(Closing());
	}

	private IEnumerator Opening()
	{
		StartCoroutine(DoorMove(_rightDoor, _rightDoorClosedPosition, _rightDoorOpenedPosition));
		yield return DoorMove(_leftDoor, _leftDoorClosedPosition, _leftDoorOpenedPosition);
		OnOpen?.Invoke();
	}

	private IEnumerator Closing()
	{
		OnClose?.Invoke();
		StartCoroutine(DoorMove(_rightDoor, _rightDoorOpenedPosition, _rightDoorClosedPosition));
		yield return DoorMove(_leftDoor, _leftDoorOpenedPosition, _leftDoorClosedPosition);
	}

	private IEnumerator DoorMove(Transform door, Vector3 from, Vector3 to)
	{
		door.localPosition = from;
		for (float t = 0; t < _doorMoveTime; t += Time.deltaTime)
		{
			door.localPosition = Vector3.Lerp(from, to, t / _doorMoveTime);
			yield return null;
		}
		door.localPosition = to;
	}

}

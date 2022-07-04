using System.Collections;
using UnityEngine;

public class LiftSystem : MonoBehaviour
{
	private Transform _player;

	private void Awake()
	{
		_player = GameObject.FindGameObjectWithTag("Player").transform;	
	}

	public void Enter(DoubleDoor liftTo)
	{
		StartCoroutine(Entering(liftTo));
	}

	private IEnumerator Entering(DoubleDoor liftTo)
	{
		ScreenEffects.Blink(0.5f, 1, 0.5f, Color.black);
		yield return new WaitForSecondsRealtime(0.5f);
		_player.position = liftTo.transform.position;
		liftTo.OpenWithoutNotify();

		yield return new WaitForSecondsRealtime(1f);
		liftTo.CloseWithoutNotify();
	}
}

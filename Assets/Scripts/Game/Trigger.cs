using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Trigger : MonoBehaviour
{
	public UnityEvent OnEnter;
	public UnityEvent OnExit;

	public bool Triggered => _triggered;

	[SerializeField] public string TargetTag;

	private bool _triggered;
	
	private void Awake()
	{
		GetComponent<BoxCollider2D>().isTrigger = true;
		TargetTag = Tags.PLAYER;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag(TargetTag))
		{
			OnEnter.Invoke();
			_triggered = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag(TargetTag))
		{
			OnExit.Invoke();
			_triggered = false;
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (_triggered) return;

		if (collision.CompareTag(TargetTag))
		{
			OnEnter.Invoke();
			_triggered = true;
		}
	}
}

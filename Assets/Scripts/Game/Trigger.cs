using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Trigger : MonoBehaviour
{
	public UnityEvent OnEnter;
	public UnityEvent OnExit;

	[SerializeField] private string _targetTag;  

	private void Awake()
	{
		GetComponent<BoxCollider2D>().isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == _targetTag)
		{
			Debug.Log("Triggered " + name);
			OnEnter.Invoke();
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == _targetTag)
		{
			Debug.Log("Ontriggered " + name);
			OnExit.Invoke();
		}
	}
}

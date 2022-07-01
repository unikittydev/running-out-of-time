using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Trigger))]
public class InteractableZone : MonoBehaviour
{
	public UnityEvent OnAvailable;
	public UnityEvent OnUnavailable;
	public UnityEvent OnInteract;

	[SerializeField] private bool _destroyOnInteract;
	[SerializeField] private bool _disableOnInteract;

	private Trigger _trigger;
	private bool _available = false;

	private void Awake()
	{
		_trigger = GetComponent<Trigger>();
		_trigger.TargetTag = Tags.player;
	}

	private void Update()
	{
		if (!_available) return;

		if (Input.GetKeyDown(Hotkeys.INTERACT))
		{
			OnInteract?.Invoke();
			if (_destroyOnInteract)
				Destroy(gameObject);
			else if (_disableOnInteract)
				Disable();
		}
	}

	public void Enable() =>
		this.gameObject.SetActive(true);
	public void Disable() =>
		this.gameObject.SetActive(false);

	private void OnEnable()
	{
		_trigger.OnEnter.AddListener(() => { _available = true; OnAvailable?.Invoke(); });
		_trigger.OnExit.AddListener(() => { _available = false; OnUnavailable?.Invoke(); });
		if (_trigger.Triggered)
			OnAvailable?.Invoke();
	}

	private void OnDisable()
	{
		_trigger.OnEnter.RemoveAllListeners();
		_trigger.OnExit.RemoveAllListeners();
		if (_available)
			OnUnavailable?.Invoke();
	}

	private void OnDestroy()
	{
		_trigger.OnEnter.RemoveAllListeners();
		_trigger.OnExit.RemoveAllListeners();
		if (_available)
			OnUnavailable?.Invoke();
	}

	private void OnValidate()
	{
		GetComponent<Trigger>().hideFlags = HideFlags.HideInInspector;
	}

}

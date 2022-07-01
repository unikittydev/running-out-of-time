using UnityEngine.Events;

public class InteractableTimeObject : TimeObject
{
	public UnityEvent OnAvailable;
	public UnityEvent OnUnavailable;

	private bool _available = false;

	private void Awake()
	{
		GoToEpoch(_time);
	}

	private void OnEnable()
	{
		if (_available)
			OnAvailable?.Invoke();
	}
	
	private void OnDisable()
	{
		if (_available)
			OnUnavailable?.Invoke();
		_available = false;
	}

	public void GoToEpoch(TimeEpoch epoch)
	{
		if (_time.HasFlag(epoch) && !_available)
		{
			_available = true;
			if (gameObject.activeSelf)
				OnAvailable?.Invoke();
		}
		else if (!_time.HasFlag(epoch) && _available)
		{
			_available = false;
			OnUnavailable?.Invoke();
		}
	}
}

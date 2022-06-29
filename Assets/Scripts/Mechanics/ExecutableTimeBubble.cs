using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExecutableTimeBubble : TimeBubble
{
    [SerializeField] private bool _showTimer = false;
    [SerializeField] private bool _loop = false;
    [SerializeField] private bool _activeOnAwake = false;
    [SerializeField] private float _duration;
    [Range(0f, 1f)]
    [SerializeField] private float _durationRandomizePercent = 0;

    [SerializeField] private Image _timerImage;

    protected override void Init()
    {
        _timerImage.gameObject.SetActive(false);
        if (_activeOnAwake) Execute();
    }

    public void Execute()
    {
        if (!_interactable) return;

        _interactable = false;
        _timerImage.gameObject.SetActive(_showTimer);
        StartCoroutine(Activation());
        StartCoroutine(Idle());
    }

	private IEnumerator Idle()
	{
        var duration = _duration + Random.Range(-_duration * _durationRandomizePercent, _duration * _durationRandomizePercent);
        for (var t = 0f; t < duration; t += Time.deltaTime)
		{
            _timerImage.fillAmount = 1 - t / duration;
            yield return null;
		}
        yield return Deactivation();
        _timerImage.gameObject.SetActive(false);
        yield return Reload();
        if (_loop) Execute();
    }
}

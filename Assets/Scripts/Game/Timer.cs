using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public int Time 
    {
        get => _time;
        private set 
        {
            if (_time != value)
			{
                _time = Mathf.Max(0, value);
                OnTimeChanged();
			}
        } 
    }
    public bool IsActive { get; private set; }

    [Header("Preferences")]
    [SerializeField] private int _time = 0;
    [SerializeField] private bool _activeOnAwake = true;
    [SerializeField] private Color _addColor = Color.green;
    [SerializeField] private Color _subtractColor = Color.red;

    [Header("Animation Preferences")]
    [SerializeField] private float _fadeTime = 0.5f;
    [SerializeField] private float _fadeChangesTime = 1f;
    [SerializeField] private Color _fadeChangesColor = Color.white;

    [Header("References")]
    [SerializeField] private RectTransform _tMinutes;
    [SerializeField] private RectTransform _minutes;
    [SerializeField] private RectTransform _tSeconds;
    [SerializeField] private RectTransform _seconds;
    [Space]
    [SerializeField] private TMPro.TMP_Text _tMinutesGen;
    [SerializeField] private TMPro.TMP_Text _minutesGen;
    [SerializeField] private TMPro.TMP_Text _tSecondsGen;
    [SerializeField] private TMPro.TMP_Text _secondsGen;
    [Space]
    [SerializeField] private TMPro.TMP_Text _tMinutesAnim;
    [SerializeField] private TMPro.TMP_Text _minutesAnim;
    [SerializeField] private TMPro.TMP_Text _tSecondsAnim;
    [SerializeField] private TMPro.TMP_Text _secondsAnim;
    [Space]
    [SerializeField] private TMPro.TMP_Text _changes;

    [Header("Events")]
    public UnityEvent OnEnd;

    private Coroutine _tickCoroutine;
    private Coroutine _changesCoroutine;

    private void Awake()
    {
        GetTimeValues(out var tM, out var m, out var tS, out var s);

        _tMinutesGen.text = tM.ToString();
        _minutesGen.text = m.ToString();
        _tSecondsGen.text = tS.ToString();
        _secondsGen.text = s.ToString();
        if (_activeOnAwake)
            Play();
    }

    private void Start()
    {
        if (TryGetComponent(out GameTimeValues c))
            Time = c.startSeconds;
    }

    public void Play()
	{
        if (IsActive) return;

        IsActive = true;
        _tickCoroutine = StartCoroutine(TimerTicker());
    }

    public void Stop()
    {
        if (!IsActive) return;

        IsActive = false;
        StopCoroutine(_tickCoroutine);
    }

    public void Add(int seconds)
    {
        if (seconds <= 0) return;

        Time += seconds;
        if (_changesCoroutine != null)
		{
            StopCoroutine(_changesCoroutine);
            _changesCoroutine = null;
        }
        _changesCoroutine = StartCoroutine(AnimateChanges("+" + seconds, _addColor));
    }

    public void Subtract(int seconds)
    {
        if (seconds <= 0) return;

        Time -= seconds;
        if (_changesCoroutine != null)
        {
            StopCoroutine(_changesCoroutine);
            _changesCoroutine = null;
        }
        _changesCoroutine = StartCoroutine(AnimateChanges("-" + seconds, _subtractColor));
    }

    private IEnumerator TimerTicker()
	{
        while (true)
        {
            yield return new WaitForSecondsRealtime(1);
            Time--;

            if (_time <= 0)
            {
                OnEnd?.Invoke();
                yield return new WaitWhile(() => _time <= 0);
            }
        }
    }

    private IEnumerator Animate(RectTransform transform, TMPro.TMP_Text gen, TMPro.TMP_Text anim, int to)
	{
        var startPos = Vector2.zero;
        var endPos = new Vector2(0, -40);
        anim.text = to.ToString();
        for (float t = 0; t < _fadeTime; t += UnityEngine.Time.deltaTime)
		{
            transform.anchoredPosition = Vector2.Lerp(startPos, endPos, t / _fadeTime);
            yield return null;
		}

        gen.text = to.ToString();
        transform.anchoredPosition = startPos;
    }

    private IEnumerator AnimateChanges(string changes, Color color)
	{
        var startPos = new Vector2(150, 0);
        var endPos = new Vector2(150, -30);
        var startColor = color;
        var endColor = _fadeChangesColor;

        _changes.rectTransform.anchoredPosition = startPos;
        _changes.text = changes;
        _changes.color = startColor;
        yield return null;

        for (float t = 0; t < _fadeChangesTime; t += UnityEngine.Time.deltaTime)
		{
            _changes.rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t / _fadeChangesTime);
            _changes.color = Color.Lerp(startColor, endColor, t / _fadeChangesTime);
            yield return null;
		}

        _changesCoroutine = null;
    }

    private void OnTimeChanged()
    {
        GetTimeValues(out var tM, out var m, out var tS, out var s);

        if (_tMinutesGen.text != tM.ToString())
            StartCoroutine(Animate(_tMinutes, _tMinutesGen, _tMinutesAnim, tM));
        if (_minutesGen.text != m.ToString())
            StartCoroutine(Animate(_minutes, _minutesGen, _minutesAnim, m));
        if (_tSecondsGen.text != tS.ToString())
            StartCoroutine(Animate(_tSeconds, _tSecondsGen, _tSecondsAnim, tS));
        if (_secondsGen.text != s.ToString())
            StartCoroutine(Animate(_seconds, _secondsGen, _secondsAnim, s));
    }

    private void GetTimeValues(out int tMin, out int min, out int tSec, out int sec)
	{
        var minutes = _time / 60;
        var seconds = _time % 60;
        tMin = minutes / 10;
        min = minutes % 10;
        tSec = seconds / 10;
        sec = seconds % 10;
	}
}

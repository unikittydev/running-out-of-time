using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class TimeBubble : MonoBehaviour
{
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    public bool IsActive { get; private set; }

    [SerializeField] private GameObject[] _forChangeLayer;
    [SerializeField] protected float _cooldown = 1f;
    [SerializeField] protected float _radius = 5;

    [Space]
    [Space]
    [SerializeField] private GameObject[] _bubbleObjects;

    protected bool _interactable = true;

    private void Awake()
    {
        foreach (var g in _bubbleObjects)
            g.SetActive(false);
    }

	private void Start()
    {
        Init();
    }

	protected IEnumerator Activation(float timeIn = 0.2f)
    {
        OnActivate?.Invoke();
        foreach (var g in _bubbleObjects)
            g.SetActive(true);
        IsActive = true;
        foreach (var g in _forChangeLayer)
            g.layer = Utils.PAST_LAYER;
        SetScale(0);
        for (float t = 0f; t < timeIn; t += Time.deltaTime)
		{
            SetScale(Mathf.Lerp(0, _radius * 2, t / timeIn));
            yield return null;
		}
        SetScale(_radius * 2);
	}

    protected IEnumerator Deactivation(float timeOut = 0.2f)
    {
        OnDeactivate?.Invoke();
        SetScale(timeOut);
        for (float t = timeOut; t > 0f; t -= Time.deltaTime)
        {
            SetScale(Mathf.Lerp(0, _radius * 2, t / timeOut));
            yield return null;
        }
        SetScale(_radius * 2);
        foreach (var g in _bubbleObjects)
            g.SetActive(false);
        IsActive = false;
        foreach (var g in _forChangeLayer)
            g.layer = Utils.PRESENT_LAYER;
    }

    protected IEnumerator Reload()
	{
        yield return new WaitForSeconds(_cooldown);
        _interactable = true;
	}

    protected abstract void Init();

    private void SetScale(float scale) =>
        transform.localScale = new Vector3(scale, scale, 1);

    private void OnDrawGizmos()
	{
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
	private void OnValidate()
    {
        SetScale(_radius * 2);
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (IsActive)
            if (collision.CompareTag(Tags.PLAYER))
                collision.gameObject.layer = Utils.PAST_LAYER;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.PLAYER))
            collision.gameObject.layer = Utils.PRESENT_LAYER;
    }
}
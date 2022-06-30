using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class TimeBubble : MonoBehaviour
{
    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    public bool IsActive { get; private set; }

    [SerializeField] protected float _cooldown = 1f;
    [SerializeField] protected float _radius = 5;

    [Space]
    [Space]
    [SerializeField] protected SpriteMask _mask;
    [SerializeField] private Image _imageBG;

    protected bool _interactable = true;

	private void Awake()
	{
        _mask.gameObject.SetActive(false);
        _imageBG.gameObject.SetActive(false);
    }
	private void Start()
    {
        Init();
    }

	protected IEnumerator Activation(float timeIn = 0.2f)
    {
        _mask.gameObject.SetActive(true);
        _imageBG.gameObject.SetActive(true);
        IsActive = true;
        SetScale(0);
        for (float t = 0f; t < timeIn; t += Time.deltaTime)
		{
            SetScale(Mathf.Lerp(0, _radius * 2, t / timeIn));
            yield return null;
		}
        SetScale(_radius * 2);
        ManageTimeObjects(TimeEpoch.Past);
	}

    protected IEnumerator Deactivation(float timeOut = 0.2f)
    {
        SetScale(timeOut);
        for (float t = timeOut; t > 0f; t -= Time.deltaTime)
        {
            SetScale(Mathf.Lerp(0, _radius * 2, t / timeOut));
            yield return null;
        }
        SetScale(_radius * 2);
        _mask.gameObject.SetActive(false);
        _imageBG.gameObject.SetActive(false);
        IsActive = false;
        ManageTimeObjects(TimeEpoch.Present);
    }

    protected IEnumerator Reload()
	{
        yield return new WaitForSeconds(_cooldown);
        _interactable = true;
	}

    protected abstract void Init();

    private void ManageTimeObjects(TimeEpoch epoch)
	{
        var objects = InteractableTimeObjectsPool.GetClosestObjects(transform.position, _radius);
        foreach (var obj in objects)
            obj.GoToEpoch(epoch);
	}

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
        var collider = GetComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = 0.5f;
    }
    
}
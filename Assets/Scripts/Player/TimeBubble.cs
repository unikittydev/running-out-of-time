using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TimeBubble : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float radius;

    private float _counter;

    [SerializeField]
    private GameObject player;

    private SpriteMask mask;
    private CircleCollider2D trigger;

    [SerializeField]
    private LayerMask present;
    [SerializeField]
    private LayerMask past;

    private void Awake()
    {
        transform.localScale = Vector3.one * radius;
        mask = GetComponent<SpriteMask>();
        trigger = GetComponent<CircleCollider2D>();
    }

    private IEnumerator CreateBubble()
    {
        mask.enabled = trigger.enabled = true;
        player.layer = Utils.GetLayerId(past);

        while (_counter < duration)
        {
            _counter += Time.deltaTime;
            yield return null;
        }

        _counter = 0f;

        player.layer = Utils.GetLayerId(present);
        mask.enabled = trigger.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && _counter == 0f)
            StartCoroutine(CreateBubble());
    }
}

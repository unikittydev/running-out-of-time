using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimeBubbleTick : MonoBehaviour
{
    public UnityEvent<int> onTick;

    [SerializeField]
    private float period;

    private Coroutine tick;

    private void OnEnable()
    {
        tick = StartCoroutine(Tick());
    }

    private void OnDisable()
    {
        if (tick != null)
            StopCoroutine(tick);

    }

    private IEnumerator Tick()
    {
        float counter = 0f;
        while (enabled)
        {
            if (counter >= 1f)
            {
                counter %= 1;
                onTick?.Invoke(GameTimeValues.bubbleTickCost);
            }
            counter += Time.deltaTime / period;
            yield return null;
        }

    }
}

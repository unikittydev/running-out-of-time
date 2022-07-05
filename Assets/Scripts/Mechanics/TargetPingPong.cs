using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPingPong : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform from;
    [SerializeField]
    private Transform to;

    [SerializeField]
    private float movingTime;

    private Coroutine move;

    private void Start()
    {
        target.position = from.position;
    }

    private void OnEnable()
    {
        move = StartCoroutine(Move());
    }

    private void OnDisable()
    {
        if (move != null)
            StopCoroutine(move);
    }

    private IEnumerator Move()
    {
        float counter;
        Transform from_ = from, to_ = to;

        while (enabled)
        {
            counter = 0f;
            while (counter < movingTime)
            {
                float t = counter / movingTime;
                target.position = Vector3.Lerp(from_.position, to_.position, t);

                counter += Time.deltaTime;
                yield return null;
            }
            Swap(ref from_, ref to_);
        }
    }

    private void Swap(ref Transform from, ref Transform to)
    {
        Transform t = from;
        from = to;
        to = t;
    }
}

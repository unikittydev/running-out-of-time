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
    private bool activeOnEnable;
    [SerializeField]
    private float movingTime;

    private Coroutine move;

    private void Start()
    {
        target.position = from.position;
    }

    private void OnEnable()
    {
        if (activeOnEnable)
            Move();
    }

    private void OnDisable()
    {
        if (move != null)
            StopCoroutine(move);
    }

    public void Move()
    {
        move = StartCoroutine(Move_(true));
    }

    public void MoveOnce()
    {
        move = StartCoroutine(Move_(false));
    }

    private IEnumerator Move_(bool loop)
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
            if (!loop)
                yield break;
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

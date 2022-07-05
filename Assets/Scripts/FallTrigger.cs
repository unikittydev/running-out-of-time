using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    private void Awake()
    {
        GetComponent<Trigger>().OnEnter.AddListener(() => timer.Subtract(GameTimeValues.fallCost));
    }
}

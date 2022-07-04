using Game;
using System.Collections;
using UnityEngine;

public class TurnPlayerAction : SequenceAction
{
    [SerializeField]
    private bool turnRight;

    [SerializeField]
    private PlayerController control;

    public override IEnumerator Execute()
    {
        float h = turnRight ? .01f : -.01f;
        control.h = h;
        yield return new WaitForFixedUpdate();
        control.h = 0f;
    }
}

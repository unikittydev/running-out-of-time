using UnityEngine;
using System.Collections;

public class SwitchableTimeBubble : TimeBubble
{
    protected override void Init() { }

    public void Execute()
    {
        StartCoroutine(Switch());
    }


    private IEnumerator Switch()
	{
        if (!_interactable) yield break;

        _interactable = false;
        if (IsActive)
            yield return Deactivation();
        else
            yield return Activation();

        StartCoroutine(Reload());
    }

}

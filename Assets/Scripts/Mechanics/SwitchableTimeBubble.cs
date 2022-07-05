using UnityEngine;
using System.Collections;

public class SwitchableTimeBubble : TimeBubble
{
    protected override void Init() { }

    public void Execute()
    {
        StartCoroutine(Switch());
    }

    public void Execute(bool enable)
    {
        StartCoroutine(Switch(enable));
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

    private IEnumerator Switch(bool enable)
    {
        if (!_interactable) yield break;

        _interactable = false;
        if (!enable)
            yield return Deactivation();
        else
            yield return Activation();

        StartCoroutine(Reload());
    }
}

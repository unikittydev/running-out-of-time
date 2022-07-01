using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TimeObject : MonoBehaviour
{
    [SerializeField] protected TimeEpoch _time = TimeEpoch.Present;

	private void OnValidate()
	{
		var renderer = GetComponent<SpriteRenderer>();
		if (_time == TimeEpoch.Present)
		{
			renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
			gameObject.layer = Utils.PRESENT_LAYER;
		}
		else
		{
			renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
			if (_time.HasFlag(TimeEpoch.Present))
				gameObject.layer = 0;
			else
				gameObject.layer = Utils.PAST_LAYER;
		}
	}
}

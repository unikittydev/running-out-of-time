using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TimeObject : MonoBehaviour
{
    [SerializeField] public TimeEpoch _time = TimeEpoch.Present;

	private void OnValidate()
	{
		var renderer = GetComponent<SpriteRenderer>();
		if (_time == TimeEpoch.Present)
			renderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
		else
			renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
	}

	private void Awake()
	{
		
	}
}

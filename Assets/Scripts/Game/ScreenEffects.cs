using Game;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour
{
	private static ScreenEffects _instance;

	[SerializeField] private Image _overlayImage;

	private PlayerController _controller;

	private void Awake()
	{
		_instance = this;
		_overlayImage.gameObject.SetActive(false);
		_controller = FindObjectOfType<PlayerController>();
	}

	public static void Blink(float inTime, float stayTime, float outTime, Color color)
	{
		_instance.StartCoroutine(Blinking(inTime, stayTime, outTime, color));
	}

	public static IEnumerator Blinking(float inTime, float stayTime, float outTime, Color color)
	{
		_instance._controller.Lock();
		var img = _instance._overlayImage;
		var transparent = new Color(color.r, color.g, color.b, 0);
		var filled = color;

		img.gameObject.SetActive(true);


		img.color = transparent;

		for (float t = 0; t < inTime; t += Time.deltaTime)
		{
			img.color = Color.Lerp(transparent, filled, t / inTime);
			yield return null;
		}

		img.color = filled;

		yield return new WaitForSecondsRealtime(stayTime);

		for (float t = 0; t < outTime; t += Time.deltaTime)
		{
			img.color = Color.Lerp(filled, transparent, t / outTime);
			yield return null;
		}

		img.color = transparent;

		img.gameObject.SetActive(false);

		_instance._controller.Unlock();
	}
}

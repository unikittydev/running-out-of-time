using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
	private static MessagePanel _instance;

	[Header("Objects references")]
	[SerializeField] private RectMask2D _mask;
	[SerializeField] private Image _BG;
	[SerializeField] private TMPro.TMP_Text _messageText;
	[SerializeField] private TMPro.TMP_Text _tabToSkip;

	[Header("Preferences")]
	[SerializeField] private KeyCode _skipKey;
	[SerializeField] private float _messagesInterval;
	[SerializeField] private float _letterInterval;
	[SerializeField] private int _maskSoftness;
	[SerializeField] private float _BGAlpha;
	[SerializeField] private float _tabToSkipAlpha;

	private Queue<string> _messages = new Queue<string>();
	private bool _isShown = false;

	private void Awake()
	{
		_instance = this;
		_messageText.text = "";
		_tabToSkip.alpha = 0;
		_BG.color = new Color(_BG.color.r, _BG.color.g, _BG.color.b, 0);
		_mask.softness = new Vector2Int(2000, 20);
	}

	public static void AddMessageInQueue(string message)
	{
		_instance._messages.Enqueue(message);
		if (!_instance._isShown)
			_instance.StartCoroutine(_instance.Opening());
	}

	private IEnumerator Opening()
	{
		_isShown = true;
		_messageText.text = "";
		_messageText.alpha = 1;
		_tabToSkip.alpha = 0;
		_BG.color = new Color(_BG.color.r, _BG.color.g, _BG.color.b, 0);

		for (float t = 0; t < 1f; t += Time.deltaTime)
		{
			_BG.color = new Color(_BG.color.r, _BG.color.g, _BG.color.b, Mathf.Lerp(0, _BGAlpha / 255, t));
			_mask.softness = new Vector2Int((int)Mathf.Lerp(2000, _maskSoftness, t), 20);
			yield return null;
		}
		_mask.softness = new Vector2Int(_maskSoftness, 20);

		for (float t = 0; t < 0.5f; t += Time.deltaTime)
		{
			_tabToSkip.alpha = Mathf.Lerp(0, _tabToSkipAlpha / 255, t * 2);
			yield return null;
		}
		_tabToSkip.alpha = _tabToSkipAlpha / 255;

		StartCoroutine(MessageShowing());
	}
	private IEnumerator MessageShowing()
	{
		while (_messages.Count > 0)
		{
			yield return MessagePrinting(_messages.Dequeue());

			for (float t = 0; t < _messagesInterval; t += Time.deltaTime)
			{
				if (Input.GetKeyDown(_skipKey))
				{
					if (_messages.Count == 0)
						yield return new WaitForSeconds(_messagesInterval);
					break;
				}
				yield return null;
			}
		}

		StartCoroutine(Closing());
	}

	private IEnumerator MessagePrinting(string message)
	{
		_messageText.text = "";
		for (int i = 0; i < message.Length; i++)
		{
			_messageText.text += message[i];

			for (float t = 0; t < _letterInterval; t += Time.deltaTime)
			{
				if (Input.GetKeyDown(_skipKey))
				{
					_messageText.text = message;
					yield return null;
					yield break;
				}
				yield return null;
			}
		}
	}

	private IEnumerator Closing()
	{
		_isShown = false;

		for (float t = 0; t < 0.5f; t += Time.deltaTime)
		{
			_tabToSkip.alpha = Mathf.Lerp(_tabToSkipAlpha / 255, 0, t * 2);
			_messageText.alpha = Mathf.Lerp(1, 0, t * 2);
			yield return null;
		}
		_messageText.alpha = 0;
		_tabToSkip.alpha = 0;

		for (float t = 0; t < 1.5f; t += Time.deltaTime)
		{
			_BG.color = new Color(_BG.color.r, _BG.color.g, _BG.color.b, Mathf.Lerp(_BGAlpha / 255, 0, t / 1.5f));
			_mask.softness = new Vector2Int((int)Mathf.Lerp(_maskSoftness, 2000, t / 1.5f), 20);
			yield return null;
		}
		_mask.softness = new Vector2Int(2000, 20);
		_BG.color = new Color(_BG.color.r, _BG.color.g, _BG.color.b, 0);

		yield return null;
	}


}

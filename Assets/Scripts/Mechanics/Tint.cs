using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tint : MonoBehaviour
{
    [SerializeField] private Sprite _buttonSprite;
    [SerializeField] private string _tint;

    [Space]
    [Header("Objects references")]
    [SerializeField] private Image _buttonImage;
    [SerializeField] private TMP_Text _tintText;

	private void Awake()
	{
        HideTint();
    }
	public void ShowTint()
    {
        gameObject.SetActive(false);
    }
    public void HideTint()
    {
        gameObject.SetActive(true);
    }

	private void OnValidate()
	{
        if (_buttonImage)
            _buttonImage.sprite = _buttonSprite;
        if (_tintText)
            _tintText.text = _tint;
	}
}

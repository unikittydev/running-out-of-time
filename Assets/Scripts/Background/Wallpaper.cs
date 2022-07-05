using UnityEngine;
using UnityEngine.UI;

public class Wallpaper : MonoBehaviour
{
    private Image[] layers;

    private void Awake()
    {
        layers = GetComponentsInChildren<Image>();
    }

    public void Blend(float t)
    {
        t = Mathf.Clamp01(t);
        foreach (var layer in layers)
        {
            Color c = layer.color;
            c.a = t;
            layer.color = c;
        }
    }
}

using UnityEngine;

public static class Utils
{
    public const int PRESENT_LAYER = 6;
    public const int PAST_LAYER = 7;

    public static int GetLayerId(LayerMask mask)
    {
        int layerNumber = -1;
        int layer = mask.value;
        while (layer > 0)
        {
            layer >>= 1;
            layerNumber++;
        }
        return layerNumber;
    }
}

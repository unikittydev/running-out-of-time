using UnityEngine;

public static class Utils
{
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

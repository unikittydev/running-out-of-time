using UnityEngine;

public static class Utils
{
    public static int PRESENT_FORE_LAYER = 6;
    public static int PRESENT_BACK_LAYER = 7;
    public static int PAST_FORE_LAYER = 8;
    public static int PAST_BACK_LAYER = 9;

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

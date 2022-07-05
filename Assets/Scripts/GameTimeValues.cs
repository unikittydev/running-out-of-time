using UnityEngine;

public class GameTimeValues : MonoBehaviour
{
    private static GameTimeValues instance;

    [SerializeField]
    private int _startSeconds;
    public int startSeconds => instance._startSeconds;

    [SerializeField]
    private int _bubbleTickCost;
    public static int bubbleTickCost => instance._bubbleTickCost;

    [SerializeField]
    private int _fallCost;
    public static int fallCost => instance._fallCost;

    [SerializeField]
    private int _bonusTimeGain;
    public static int bonusTimeGain => instance._bonusTimeGain;

    private void Awake()
    {
        instance = this;
    }
}

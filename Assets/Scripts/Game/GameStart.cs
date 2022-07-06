using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform start;

    private void Start()
    {
        player.position = start.position;
    }
}

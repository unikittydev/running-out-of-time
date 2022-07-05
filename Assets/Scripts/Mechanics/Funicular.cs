using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funicular : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private SpriteRenderer[] forwardSprites;

    private const int layerOffset = 100;

    private bool moving;

    public void StartMoving()
    {
        foreach (var sprites in forwardSprites)
            sprites.sortingOrder += layerOffset;
        moving = true;
    }

    public void StopMoving()
    {
        foreach (var sprites in forwardSprites)
            sprites.sortingOrder -= layerOffset;
        moving = false;
    }

    private void Update()
    {
        if (moving)
            player.position = target.position;
    }
}

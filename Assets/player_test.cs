using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_test : MonoBehaviour
{
    [SerializeField]
    private float speedX = 14f;
    [SerializeField]
    private float speedY = 10f;

    Rigidbody2D rb;
    SpriteRenderer sprite;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Fly()
    {
        Vector3 dirX = transform.right * Input.GetAxis("Horizontal");

        Vector3 dirY = transform.up * Input.GetAxis("Vertical");

        sprite.flipX = dirX.x < 0.0f;

        rb.MovePosition(transform.position + (dirX * speedX + dirY * speedY) * Time.fixedDeltaTime);

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frameS
    void FixedUpdate()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) Fly();
    }
}

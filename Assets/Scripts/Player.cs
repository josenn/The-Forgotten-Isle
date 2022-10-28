using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1.0f;
    private Rigidbody2D rb2D;
    private SpriteRenderer playerSprite;
    private Vector2 inputVector = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    // Flip the Sprite horizontally if moving left or right
    void FlipSprite(float axis)
    {
        if(axis > 0)
        {
            playerSprite.flipX = true;
        }
        if(axis < 0)
        {
            playerSprite.flipX = false;
        }
    }

    void Movement()
    {
        Vector2 pos = rb2D.position;
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        inputVector.Normalize();

        FlipSprite(inputVector.x);

        Vector2 newPos = pos + inputVector * movementSpeed * Time.fixedDeltaTime;

        rb2D.MovePosition(newPos);
    }
}

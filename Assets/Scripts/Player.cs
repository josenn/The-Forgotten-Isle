using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1.0f;
    private Rigidbody rb;
    public SpriteRenderer playerSprite;
    private Vector3 inputVector = Vector3.zero;
    public Camera playerCamera;

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
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

    // Moves the player
    void Movement()
    {
        Vector3 pos = rb.position;
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");
        inputVector.Normalize();
        
        

        FlipSprite(inputVector.x);

        Vector3 newPos = pos + inputVector * movementSpeed * Time.fixedDeltaTime;

        rb.MovePosition(newPos);
    }
}

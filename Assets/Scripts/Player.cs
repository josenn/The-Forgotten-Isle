using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private Rigidbody rb;
    private CharacterController controller;
    private SpriteRenderer playerSprite;
    private Vector3 inputVector = Vector3.zero;
    private Vector3 playerVelocity = Vector3.zero;
    [SerializeField] float playerSpeed = 1.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        if(controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        PlayerJump();

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), playerVelocity.y, Input.GetAxisRaw("Vertical"));
        Vector3 direction = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

    //     if (direction != Vector3.zero)
    //     {
    //         gameObject.transform.forward = direction;
    //     }
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
    }
}

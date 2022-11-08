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
    public Camera playerCamera;
    [SerializeField] DialogueUI dialogueUI;
    public Animator spriteAnimator;
    private float lastMoveH;
    private float lastMoveV;
    public float idleSetDelay = 0.09f;
    private bool isWalking = false;
    private float movementX;
    private float movementZ;
    bool isStopped = true;
    public bool allowedToMove = true;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }

    //getting directional info from the camera
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
        controller = GetComponent<CharacterController>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (allowedToMove){
            PlayerMove();
        }

        if(dialogueUI.IsOpen) return;
        if(Input.GetKeyDown(KeyCode.F))
        {
            Interactable?.Interact(this);
        }

        if (controller.velocity.x == 0f && controller.velocity.z == 0f){
            isStopped = true;
        }
        else{
            isStopped = false;
        }
        spriteAnimator.SetBool("beenStopped", isStopped);

        spriteAnimator.SetFloat("LastMoveHorizontal", lastMoveH);
        spriteAnimator.SetFloat("LastMoveVertical", lastMoveV);
        
        spriteAnimator.SetBool("isWalking", isWalking);
        //Set the parameters of the animator's blend tree to our inputs along with camera influence
        spriteAnimator.SetFloat("Horizontal", movementX);
        spriteAnimator.SetFloat("Vertical", movementZ);
        //start coroutine to get delayed movement data for setting direction of idle state
        if(isWalking){
           StartCoroutine(lastMoveSet(movementX, movementZ)); 
        }

        Debug.Log("X: " + movementX + ". Z: " + movementZ + ".");
    }

    void PlayerMove()
    {
        if(controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        PlayerJump();
        
        
        Vector3 forwardRelativeVerticalInput = Input.GetAxisRaw("Vertical") * GetCameraForward(playerCamera);
        Vector3 rightRelativeHorizontalInput = Input.GetAxisRaw("Horizontal") * GetCameraRight(playerCamera);
        Vector3 addJump = new Vector3(0, playerVelocity.y, 0);
        Vector3 move = forwardRelativeVerticalInput + rightRelativeHorizontalInput + addJump;
        Vector3 direction = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        movementX = (Input.GetAxisRaw("Horizontal") * Mathf.Abs(GetCameraRight(playerCamera).x));
        movementZ = (Input.GetAxisRaw("Vertical") * Mathf.Abs(GetCameraForward(playerCamera).z));

        
        

        
        

        //for switching the animator to idle
        if (movementX == 0f && movementZ == 0f)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }


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

    private IEnumerator lastMoveSet(float moveX, float moveZ){
    
        
        lastMoveH = Mathf.Lerp(lastMoveH, moveX, Time.deltaTime * idleSetDelay);
        lastMoveV = Mathf.Lerp(lastMoveV, moveZ, Time.deltaTime * idleSetDelay);

        yield return null;

    }
}

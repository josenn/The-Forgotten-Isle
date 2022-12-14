using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private SpriteRenderer playerSprite;
    private Vector3 inputVector = Vector3.zero;
    private Vector3 playerVelocity = Vector3.zero;
    [SerializeField] private float playerSpeed = 1.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityMultiplier = 4f;
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
    private bool isJumping = false;
    private bool hasLanded = true;
    private bool _isRunning = false;
    public bool allowedToMove = true;
    public Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;
    public bool allowedToInteract = true;
    private Animator fadeWhiteAnim;
    private Animator fadeBlackAnim;
    private Animator textFadeAnim;
    private TimeDialActivator _timeDial;
    private transport_player _transport;
    public Animator sunAnim;
    private Respawn_Handler _respawnHandler;
    public float runSpeed;
    private float _normalWalkSpeed;

    public AudioClip jumpSFX, landSFX, worldChangeSFX, deathSFX;
    public AudioClip[] grassStep, snowStep;
    public float walkSFXSpeed = 0.3f, runSFXSpeed = 0.1f;
    private float newStep = 0;
    private AudioSource source;

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


    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerSprite = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        fadeWhiteAnim = GameObject.FindGameObjectWithTag("FadeWhite").GetComponent<Animator>();
        fadeBlackAnim = GameObject.FindGameObjectWithTag("FadeBlack").GetComponent<Animator>();
        textFadeAnim = GameObject.FindGameObjectWithTag("TextFade").GetComponent<Animator>();
        inventory = new Inventory(); 
        uiInventory.SetInventory(inventory);
        _respawnHandler = GameObject.Find("*Respawn Handler").GetComponent<Respawn_Handler>();
        _normalWalkSpeed = playerSpeed;
    }


    void OnTriggerStay (Collider other){
        if (other.CompareTag("TimeDial")) {
            TimeDialActivator timeDial = other.gameObject.GetComponent<TimeDialActivator>();
            if (allowedToInteract) {
                if(Input.GetKeyDown(KeyCode.F)){
                    if (timeDial.delayOver && timeDial.dialHasCrystal && timeDial.sisterHasCrystal){
                        allowedToMove = false;
                        allowedToInteract = false;
                        _timeDial = timeDial;
                        source.clip = worldChangeSFX;
                        source.PlayOneShot(source.clip);
                        StartCoroutine(TimeDialEffects());
                    }
                }
            }
        }
        if (other.CompareTag("Transporter")){
            transport_player transport = other.gameObject.GetComponent<transport_player>();
            if (allowedToInteract) {
                if (Input.GetKeyDown(KeyCode.F)){
                    allowedToInteract = false;
                    allowedToMove = false;
                    _transport = transport;
                    StartCoroutine(TransportThePlayer());
                }
            }
        }
        if (other.CompareTag("EndGame")){
            if (allowedToInteract) {
                if (Input.GetKeyDown(KeyCode.F)){
                    allowedToInteract = false;
                    allowedToMove = false;
                    fadeBlackAnim.SetTrigger("FadeOut");
                    textFadeAnim.SetTrigger("FadeText");
                }
            }
        }
        
        if (other.CompareTag("TropicalZone"))
        {
            if (newStep <= 0)
            {
                if (_isRunning && isWalking && !isJumping)
                {
                    PlayStep(grassStep);
                    newStep = runSFXSpeed;
                }
                else if (isWalking && !isJumping)
                {
                    PlayStep(grassStep);
                    newStep = walkSFXSpeed;
                }
            }
            else
            {
                newStep -= Time.deltaTime;
            }
        }
        if (other.CompareTag("FrozenZone"))
        {
            if (newStep <= 0)
            {
                if (_isRunning && isWalking && !isJumping)
                {
                    PlayStep(snowStep);
                    newStep = runSFXSpeed;
                }
                else if (isWalking && !isJumping)
                {
                    PlayStep(snowStep);
                    newStep = walkSFXSpeed;
                }
            }
            else
            {
                newStep -= Time.deltaTime;
            }
        }
    }

    private void PlayStep(AudioClip[] footstep)
    {
        source.clip = footstep[Random.Range(0, footstep.Length)];
        source.PlayOneShot(source.clip);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("RespawnOnContact")){
            
            StartCoroutine(RespawnPlayer());
            Debug.Log("player respawned by " + other.name);
        }
    }
    private IEnumerator RespawnPlayer(){
        source.clip = deathSFX;
        source.PlayOneShot(source.clip);
        allowedToMove = false;
        allowedToInteract = false;
        yield return new WaitForSeconds(0.01f);
        transform.position = _respawnHandler.currentRespawn.position;
        yield return new WaitForSeconds(0.1f);
        allowedToMove = true;
        allowedToInteract = true;
    }

    private IEnumerator TransportThePlayer(){
        fadeBlackAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        transform.position = _transport.destination.position;
        yield return new WaitForSeconds(3f);
        fadeBlackAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3f);
        allowedToInteract = true;
        allowedToMove = true;
    }

    private IEnumerator TimeDialEffects(){
        sunAnim.SetTrigger("SpinSun");
        yield return new WaitForSeconds(1f);
        fadeWhiteAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        transform.position = _timeDial.teleportDestination.position;
        yield return new WaitForSeconds(1f);
        fadeWhiteAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        allowedToMove = true;
        allowedToInteract = true;
    }


    void Update()
    {
        // if (Interactable != null){
        //     Debug.Log(Interactable.ToString());
        // }

        if (allowedToMove){
            PlayerMove();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)){
            playerSpeed = runSpeed;
            _isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            playerSpeed = _normalWalkSpeed;
            _isRunning = false;
        }


        if (controller.isGrounded)
        {
            isJumping = false;
            if (hasLanded == true)
            {
                source.clip = landSFX;
                source.PlayOneShot(source.clip);
                hasLanded = false;
            }
        }

        if(dialogueUI.IsOpen) return;
        if (allowedToInteract) {
            if(Input.GetKeyDown(KeyCode.F))
            {
                Interactable?.Interact(this);
            }
        }

        if (controller.velocity.x == 0f && controller.velocity.z == 0f){
            isStopped = true;
        }
        else{
            isStopped = false;
        }
        spriteAnimator.SetBool("beenStopped", isStopped);
        spriteAnimator.SetBool("isRunning", _isRunning);

        spriteAnimator.SetFloat("LastMoveHorizontal", lastMoveH);
        spriteAnimator.SetFloat("LastMoveVertical", lastMoveV);
        
        spriteAnimator.SetBool("isWalking", isWalking);
        spriteAnimator.SetBool("isJumping", isJumping);

        //Set the parameters of the animator's blend tree to our inputs along with camera influence
        spriteAnimator.SetFloat("Horizontal", movementX);
        spriteAnimator.SetFloat("Vertical", movementZ);
        
        //start coroutine to get delayed movement data for setting direction of idle state
        if (isWalking){
           StartCoroutine(lastMoveSet(movementX, movementZ)); 
        }

        //Debug.Log("X: " + movementX + ". Z: " + movementZ + ".");
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
        Vector3 move = (forwardRelativeVerticalInput * playerSpeed) + (rightRelativeHorizontalInput * playerSpeed) + (addJump * _normalWalkSpeed);
        Vector3 direction = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        controller.Move(move * Time.deltaTime);

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
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -gravityMultiplier * gravityValue);
            isJumping = true;

            source.clip = jumpSFX;
            source.PlayOneShot(source.clip);
            hasLanded = true;
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
    }

    private IEnumerator lastMoveSet(float moveX, float moveZ){
    
        
        lastMoveH = Mathf.Lerp(lastMoveH, moveX, Time.deltaTime * idleSetDelay);
        lastMoveV = Mathf.Lerp(lastMoveV, moveZ, Time.deltaTime * idleSetDelay);

        yield return null;

    }

    public void PlayPickup(AudioClip sfx)
    {
        source.clip = sfx;
        source.PlayOneShot(source.clip);
    }
}

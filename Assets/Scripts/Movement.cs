
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float playerSpeed = 6.0f;
    public float lookSpeed = 2.0f;
    public float gravityForce = 9.8f;
    public Transform cameraTransform;

    public Rigidbody rb;

    [SerializeField]
    private SpriteRenderer sr;

    public Animator animator;

    public Sprite[] directionSprites;

    private bool isUnderwater = false;
    private Vector3 moveDirection = Vector3.zero;
    public bool isGrounded = false;
    private float cameraPitch = 0.0f;

    public float underwaterGravityMultiplier = 0.3f;
    public float jumpForce = 5.0f;
    public bool isJumping = false;
    private float jumpTimeCounter = 0.0f;

    public float jumpDuration = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

void UpdateSpriteDirection(float moveX, float moveZ)
{
    if (moveX > 0 && moveZ > 0)
    {
        sr.sprite = directionSprites[1]; // Facing top-right
    }
    else if (moveX > 0 && moveZ < 0)
    {
        sr.sprite = directionSprites[4]; // Facing bottom-right
    }
    else if (moveX < 0 && moveZ > 0)
    {
        sr.sprite = directionSprites[1]; // Facing top-left
    }
    else if (moveX < 0 && moveZ < 0)
    {
        sr.sprite = directionSprites[5]; // Facing bottom-left
    }
    else if (moveX > 0)
    {
        sr.sprite = directionSprites[2]; // Facing right
    }
    else if (moveX < 0)
    {
        sr.sprite = directionSprites[3]; // Facing left
    }
    else if (moveZ > 0)
    {
        sr.sprite = directionSprites[1]; // Facing up
    }
    else if (moveZ < 0)
    {
        // animator.SetBool("S_Pressed", true); // Facing down
    }
}

void Update()
{
    // Handle player movement
    float moveX = Input.GetAxis("Horizontal");
    float moveZ = Input.GetAxis("Vertical");

    Vector3 move = transform.right * moveX + transform.forward * moveZ;
    moveDirection = move * playerSpeed;

    UpdateSpriteDirection(moveX, moveZ);

    // Modify variables for animation
    if (Input.GetKeyDown(KeyCode.W)) {
        animator.SetBool("W_Pressed", true);
    }
    if (Input.GetKeyDown(KeyCode.A)) {
        animator.SetBool("A_Pressed", true);
    }
    if (Input.GetKeyDown(KeyCode.S)) {
        animator.SetBool("S_Pressed", true);
    }
    if (Input.GetKeyDown(KeyCode.D)) {
        animator.SetBool("D_Pressed", true);
    }
    if (Input.GetKeyUp(KeyCode.W)) {
        animator.SetBool("W_Pressed", false);
    }
    if (Input.GetKeyUp(KeyCode.A)) {
        animator.SetBool("A_Pressed", false);
    }
    if (Input.GetKeyUp(KeyCode.S)) {
        animator.SetBool("S_Pressed", false);
    }
    if (Input.GetKeyUp(KeyCode.D)) {
        animator.SetBool("D_Pressed", false);
    }
    if (Input.GetKeyDown(KeyCode.Space)) {
        animator.SetBool("Space_Pressed", true);
    }
    if (Input.GetKeyUp(KeyCode.Space)) {
        animator.SetBool("Space_Pressed", false);
    }

    // Apply gravity or buoyancy
    if (isUnderwater)
    {
        // Handle rising and submerging
        if (Input.GetKey(KeyCode.Space))
        {
            moveDirection.y = playerSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            moveDirection.y = -playerSpeed;
        }
        else
        {
            moveDirection.y -= gravityForce * underwaterGravityMultiplier * Time.deltaTime;
        }
    }
    else
    {
        Debug.Log(isGrounded);
        if (!isGrounded)
        {
            moveDirection.y = rb.velocity.y - gravityForce * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Check for jump input
        {
            isJumping = true;
            jumpTimeCounter = 0.0f;
            Debug.Log("Jumping");
        }
    }
    rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

    //if (moveX > 0)
    //{
       // sr.flipX = false; // Facing right
    //}
    //else if (moveX < 0)
    //{
       //sr.flipX = true; // Facing left
    //}

    // Handle camera rotation
    float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
    float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

    // Rotate the camera around the character
    cameraPitch -= mouseY;
    cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

    cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0.0f, 0.0f);
    transform.Rotate(Vector3.up * mouseX);
}

void FixedUpdate()
{
    if (isJumping)
    {
        if (jumpTimeCounter < jumpDuration)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
            jumpTimeCounter += Time.fixedDeltaTime;
        }
        else
        {
            isJumping = false;
        }
    }
}

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = true;
    }
}

void OnCollisionExit(Collision collision)
{
    if (collision.gameObject.CompareTag("Ground"))
    {
        isGrounded = false;
    }
}

void OnTriggerEnter(Collider other)
{
    if (LayerMask.LayerToName(other.gameObject.layer) == "Water")
    {
        isUnderwater = true;
        
        Debug.Log("Underwater");
    }
}

void OnTriggerExit(Collider other)
{
    if (LayerMask.LayerToName(other.gameObject.layer) == "Water")
    {
        isUnderwater = false;
        
        Debug.Log("Out of Water");
    }
}

void playAnimation(AnimationClip animationClip) {
    AnimatorOverrideController overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
    overrideController["BaseAnimation"] = animationClip;
    animator.runtimeAnimatorController = overrideController;
}
}


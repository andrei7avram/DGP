/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float groundDist;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private bool isUnderwater = false;
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1f;

        if(Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, groundMask) && !isUnderwater)
        {
           if(hit.collider != null) {
            Vector3 movePos = transform.position;
            movePos.y = hit.point.y + groundDist;
            transform.position = movePos;

           }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        if(isUnderwater)
        {
            speed = 5;
            
            if(Input.GetKey(KeyCode.Space))
            {
                moveDir.y = 1;
            }else if(Input.GetKey(KeyCode.LeftControl))
            {
                moveDir.y = -1;
            }
        }
        rb.velocity = moveDir * speed;

        if(x!=0 && x>0)
        {
            sr.flipX = false;
        }
        else if(x!=0 && x<0)
        {
            sr.flipX = true;
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Water")
        {
            isUnderwater = true;
            rb.useGravity = false;
            Debug.Log("Underwater");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Water")
        {
            isUnderwater = false;
            rb.useGravity = true;
            Debug.Log("Not Underwater");
        }
    }

}*/

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

    private bool isUnderwater = false;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded = false;
    private float cameraPitch = 0.0f;

    public float underwaterGravityMultiplier = 0.3f;

    void Start()
{
    rb = GetComponent<Rigidbody>();
    if (cameraTransform == null)
    {
        cameraTransform = Camera.main.transform;
    }
}

void Update()
{
    // Handle player movement
    float moveX = Input.GetAxis("Horizontal");
    float moveZ = Input.GetAxis("Vertical");

    Vector3 move = transform.right * moveX + transform.forward * moveZ;
    moveDirection = move * playerSpeed;

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
        if (!isGrounded)
        {
            moveDirection.y = rb.velocity.y - gravityForce * Time.deltaTime;
        }
        else
        {
            moveDirection.y = rb.velocity.y; // Preserve the current vertical velocity when grounded
        }
    }

    // Move the player
    rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

    if (moveX > 0)
    {
        sr.flipX = false; // Facing right
    }
    else if (moveX < 0)
    {
        sr.flipX = true; // Facing left
    }

    // Handle camera rotation
    float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
    float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

    // Rotate the camera around the character
    cameraPitch -= mouseY;
    cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

    cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0.0f, 0.0f);
    transform.Rotate(Vector3.up * mouseX);
}

void OnCollisionStay(Collision collision)
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
}


using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab;
    public float attackCooldown = 0.5f;

    public float lungeForce = 3f;
    public float upwardForce = 2f;
    public float lungeDuration = 0.7f;

    public float groundCheckDistance = 1.5f;  // Adjust this based on turtle height
    public LayerMask groundLayer;  // Assign a layer for ground detection

    private bool canAttack = true;
    public Movement movement;

    public Rigidbody rb;
    private PhysicMaterial noFrictionMaterial;
    private PhysicMaterial defaultMaterial;

    public bool isDashing = false;

    private float dashTimeCounter = 0f;

    private void Start()
    {
        // Cache default material and create a no-friction material for lunge
        defaultMaterial = GetComponent<Collider>().material;
        noFrictionMaterial = new PhysicMaterial
        {
            dynamicFriction = 0f,
            staticFriction = 0f,
            frictionCombine = PhysicMaterialCombine.Minimum
        };
    }

    void Update()
    {
        // Only allow attacking if the turtle is grounded
        if (Input.GetMouseButtonDown(0) && canAttack)
        {   
            canAttack = false;
            StartCoroutine(AttackEnemy());
        }
    }

    IEnumerator AttackEnemy()
    {
        //GetComponent<Collider>().material = noFrictionMaterial;

        Vector3 lungeDirection = CalculateLungeDirection();
        if(!movement.isGrounded) 
        {
            isDashing = true;
            rb.AddForce(lungeDirection, ForceMode.Impulse);
            dashTimeCounter = 0f;
        }

        attackPrefab.GetComponent<Collider>().enabled = true;

        yield return new WaitForSeconds(lungeDuration);  // Wait for the duration of the lunge

        //GetComponent<Collider>().material = defaultMaterial;

        yield return new WaitForSeconds(attackCooldown - lungeDuration);  // Wait for cooldown

        attackPrefab.GetComponent<Collider>().enabled = false;
        canAttack = true;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {   
            if (movement.isGrounded)
            {
                //rb.velocity = new Vector3(0, rb.velocity.y, 0);
                isDashing = false;
            }
            if (dashTimeCounter < lungeDuration)
            {
                Vector3 lungeDirection = CalculateLungeDirection();
                //rb.AddForce(lungeDirection * (Time.fixedDeltaTime / lungeDuration), ForceMode.VelocityChange);
                dashTimeCounter += Time.fixedDeltaTime;
            }
            else
            {
                isDashing = false;
            }
        }
    }

    Vector3 CalculateLungeDirection()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            Vector3 groundNormal = hit.normal;

            float slopeAngle = Vector3.Angle(groundNormal, Vector3.up);

            float adjustedUpwardForce = Mathf.Clamp(1f - (slopeAngle / 90f), 0f, 1f) * upwardForce;

            Vector3 forwardLunge = Vector3.ProjectOnPlane(transform.forward, groundNormal).normalized;

            Vector3 lungeDirection = forwardLunge * lungeForce + transform.up * adjustedUpwardForce;

            return lungeDirection;
        }

        return transform.forward * lungeForce + transform.up * upwardForce;
    }

}

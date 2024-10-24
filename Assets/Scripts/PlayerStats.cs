using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab;

    public Stats statsRef;
    public float attackCooldown = 0.5f;

    public float lungeForce = 3f;
    public float upwardForce = 2f;
    public float lungeDuration = 0.7f;

    public float groundCheckDistance = 1.5f;  // Adjust this based on turtle height
    public LayerMask groundLayer;  // Assign a layer for ground detection

    private bool canAttack = true;
    public Movement movement;
    
    public animatorScript animatorRef;
    public Rigidbody rb;
    private PhysicMaterial noFrictionMaterial;
    private PhysicMaterial defaultMaterial;

    public bool isDashing = false;

    private float dashTimeCounter = 0f;
    public bool isShielded = false;

    public bool isShieldedAnim = false;

    public bool isAttacking = false;

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
        if (Input.GetMouseButtonDown(0) && canAttack && !isShieldedAnim && movement.IsInAttackPosition()) {   

            canAttack = false;
            StartCoroutine(AttackEnemy());

        } else if (Input.GetKeyDown(KeyCode.LeftControl) && statsRef.currentHunger >= 5) {

            statsRef.TakeHunger(5);
            StartCoroutine(Shield(true));

        } else if (Input.GetKeyUp(KeyCode.LeftControl)) {

            StartCoroutine(LoseShield(false));
        }
    }

    IEnumerator AttackEnemy()
    {
        //GetComponent<Collider>().material = noFrictionMaterial;
        isAttacking = true;
        Vector3 lungeDirection = CalculateLungeDirection();
        if(!movement.isGrounded && statsRef.currentHunger >= 5) 
        {
            isDashing = true;
            rb.AddForce(lungeDirection, ForceMode.Impulse);
            statsRef.TakeHunger(5);
            dashTimeCounter = 0f;
        }

        attackPrefab.GetComponent<Collider>().enabled = true;
        movement.rb.velocity = Vector3.zero;
        //movement.enabled = false;

        yield return new WaitForSeconds(lungeDuration);  // Wait for the duration of the lunge

        //GetComponent<Collider>().material = defaultMaterial;

        yield return new WaitForSeconds(attackCooldown - lungeDuration);  // Wait for cooldown

        //movement.enabled = true;
        attackPrefab.GetComponent<Collider>().enabled = false;
        isAttacking = false;
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

    IEnumerator Shield(bool shielded)
    {   
        isShieldedAnim = shielded;
        animatorRef.animator.SetBool("CTRL_Pressed", true);
        movement.enabled = false;
        yield return new WaitForSeconds(0.4f);
        isShielded = shielded;
    }

    IEnumerator LoseShield(bool shielded)
    {   
        isShielded = shielded;
        animatorRef.animator.SetBool("CTRL_Pressed", false);
        animatorRef.animator.SetBool("EXIT_Shield", true);
        yield return new WaitForSeconds(1.25f);
        animatorRef.animator.SetBool("EXIT_Shield", false);
        movement.enabled = true;
        isShieldedAnim = shielded;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{   
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public GameObject meleeAttack;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    Vector3 distanceToPlayer;

    public Animator animator;

    public bool meleeAttackActive = false;

    //Attacks
    public GameObject projectile;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake() {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        distanceToPlayer = transform.position - player.position;

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Patrolling() {
        animator.SetBool("crab_idle" , false);
        animator.SetBool("crab_move" , true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 2f) walkPointSet = false;
    }

    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 4f, whatIsGround)) walkPointSet = true;
    }

    private void Chasing() {
        agent.SetDestination(player.position);
    }

    private void Attacking() {

        if (meleeAttackActive) {
            Debug.Log("Melee Attack Hit");
            Debug.Log(distanceToPlayer.magnitude);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            meleeAttackActive = false;
        }
        animator.SetBool("crab_move" , false);
        animator.SetBool("crab_idle", true);
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked) {
            int attackType = Random.Range(0, 3);
            if (attackType == 0 || attackType == 1) {
                Debug.Log("Projectile Attack");
                Vector3 projectilePosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                Rigidbody rb = Instantiate(projectile, projectilePosition, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
                rb.AddForce(transform.up * 2f, ForceMode.Impulse);
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }else {
                Debug.Log("Melee Attack");
                meleeAttack.GetComponent<Collider>().enabled = true;
                attackRange = 1f;
                meleeAttackActive = true;

            }
            
        }
    }

    IEnumerator MeleeAttack() {
        yield return new WaitForSeconds(1f);
        meleeAttack.GetComponent<Collider>().enabled = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    private void ResetAttack() {
        alreadyAttacked = false;
        meleeAttack.GetComponent<Collider>().enabled = false;
        attackRange = 7.9f;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}

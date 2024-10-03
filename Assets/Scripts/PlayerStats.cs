using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab;
    public float attackCooldown = 0.5f;

    public float lungeForce = 0.3f; // Increased the lunge force
    public float upwardForce = 0.2f; // Increased the upward force
    public float lungeDuration = 0.3f;

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(AttackEnemy());
        }
    }

    IEnumerator AttackEnemy()
    {
        canAttack = false;

       float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        attackPrefab.GetComponent<Collider>().enabled = true;
        Vector3 targetPosition = initialPosition + transform.forward * lungeForce + transform.up * upwardForce;

        while (elapsedTime < lungeDuration)
        {
            // Calculate the interpolation factor based on the elapsed time
            float t = elapsedTime / lungeDuration;
            // Use a quadratic function to create an accelerating effect
            float accelerationFactor = t * t;

            // Interpolate the position
            transform.position = Vector3.Lerp(initialPosition, targetPosition, accelerationFactor);

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        

        Debug.Log("Attacking"); 

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;

        attackPrefab.GetComponent<Collider>().enabled = false;
    }
}

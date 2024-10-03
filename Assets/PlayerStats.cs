using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab;
    public float attackCooldown = 0.5f;

    private bool canAttack = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;

        attackPrefab.GetComponent<Collider>().enabled = true;

        Debug.Log("Attacking"); 

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;

        attackPrefab.GetComponent<Collider>().enabled = false;
    }
}

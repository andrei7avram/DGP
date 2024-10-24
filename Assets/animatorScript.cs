using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorScript : MonoBehaviour
{
    public Animator animator;

    public PlayerAttack playerStats;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            animator.SetBool("W_Pressed", true);
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            animator.SetBool("A_Pressed", true);
        }
        if (Input.GetKeyDown(KeyCode.S) ) {
            animator.SetBool("S_Pressed", true);
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            animator.SetBool("D_Pressed", true);
        }
        if (Input.GetKeyUp(KeyCode.W) ) {
            animator.SetBool("W_Pressed", false);
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            animator.SetBool("A_Pressed", false);
        }
        if (Input.GetKeyUp(KeyCode.S) ) {
            animator.SetBool("S_Pressed", false);
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            animator.SetBool("D_Pressed", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) ) {
            animator.SetBool("Space_Pressed", true);
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            animator.SetBool("Space_Pressed", false);
        }
        if (Input.GetMouseButtonDown(0)) {
            animator.SetBool("MOUSE1_Pressed", true);
            StartCoroutine(ResetAttack());
        }
        
    }

    IEnumerator ResetAttack() {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("MOUSE1_Pressed", false);
    }
}

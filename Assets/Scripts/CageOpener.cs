using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpener : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    [SerializeField] private GameObject obj;

    void Awake () {
        animator = obj.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnTriggerStay  (Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.E)) {
                animator.SetBool("open", true);
            }
        }
    }
}

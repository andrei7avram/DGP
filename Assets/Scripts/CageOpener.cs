using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageOpener : MonoBehaviour
{
    // Start is called before the first frame update
    Animator cageAnimator;
    Animator prisonerAnimator;
    [SerializeField] private GameObject CageObject;
    [SerializeField] private GameObject PrisonerObject;

    void Awake () {
        cageAnimator = CageObject.GetComponent<Animator>();
        prisonerAnimator = PrisonerObject.GetComponent<Animator>();
    }

    private void OnTriggerStay (Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.E)) {
                cageAnimator.SetBool("open", true);
                prisonerAnimator.SetBool("run", true);
            }
        }
    }
}

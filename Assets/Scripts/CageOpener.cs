using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CageOpener : MonoBehaviour
{
    // Start is called before the first frame update
    Animator cageAnimator;
    Animator prisonerAnimator;

    private bool isCageOpen = false;

    public Stats stats;

    public Canvas helpText;
    [SerializeField] private GameObject CageObject;
    [SerializeField] private GameObject PrisonerObject;

    void Awake () {
        cageAnimator = CageObject.GetComponent<Animator>();
        prisonerAnimator = PrisonerObject.GetComponent<Animator>();
        stats = GameObject.Find("Player").GetComponent<Stats>();
        helpText = GameObject.Find("OpenCage").GetComponent<Canvas>();
    }

    private void OnTriggerStay (Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKeyDown(KeyCode.E) && !isCageOpen) {
                cageAnimator.SetBool("open", true);
                stats.updateHermits();
                helpText.enabled = false;
                prisonerAnimator.SetBool("run", true);
                
                isCageOpen = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCageOpen)
        {
            helpText.enabled = true;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag("Player")) {
            helpText.enabled = false;
        }
    }
}

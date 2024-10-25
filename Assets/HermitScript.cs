using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HermitScript : MonoBehaviour
{   
    public Animator cageAnimator;
    public GameObject bigPrefab;
    public bool hasConversation;
  
    void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "Player" && hasConversation == false) {
            cageAnimator.SetBool("saved", true);
            Destroy(bigPrefab, 6);
        }
   }

   public void freeHermit() {
        cageAnimator.SetBool("saved", true);
        Destroy(bigPrefab, 3);
   }
}

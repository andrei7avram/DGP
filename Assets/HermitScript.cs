using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HermitScript : MonoBehaviour
{   
    public Animator cageAnimator;
  
    void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "Player") {
            cageAnimator.SetBool("saved", true);
        }
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground") {
            Destroy(gameObject);
        }
   }
}

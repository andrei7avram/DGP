using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject attackPrefab;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<HittableStats>().TakeDamage(40);
            attackPrefab.GetComponent<Collider>().enabled = false;
            
        }
    }
}

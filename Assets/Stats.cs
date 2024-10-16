using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Projectile") {
            TakeDamage(30);
            Destroy(collision.gameObject);
        }else if (collision.gameObject.tag == "Attack") {
            TakeDamage(10);
        }else if (collision.gameObject.tag == "Hazard") {
            TakeDamage(20);
        }
    }
}

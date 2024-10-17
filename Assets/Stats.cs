using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Canvas HealthBar;
    public Image[] Hearts;
    public Sprite[] HeartSprites;

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

        UpdateHearts();
    }

   void UpdateHearts()
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (currentHealth >= (i + 1) * 10)
            {
                Hearts[i].sprite = HeartSprites[2]; // Full heart
            }
            else if (currentHealth >= (i * 10) + 5)
            {
                Hearts[i].sprite = HeartSprites[1]; // Half heart
            }
            else
            {
                Hearts[i].sprite = HeartSprites[0]; // Empty heart
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Projectile") {
            Debug.Log("Projectile hit player");
            TakeDamage(35);
            Destroy(collision.gameObject);
        }else if (collision.gameObject.tag == "EnemyAttack") {
            TakeDamage(10);
        }else if (collision.gameObject.tag == "Hazard") {
            Debug.Log("Hazard hit player");
            TakeDamage(15);
        }
    }
}

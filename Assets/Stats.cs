using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public float maxHunger = 100f;

    public float currentHunger;

    public Canvas HealthBar;
    public Image[] Hearts;
    public Sprite[] HeartSprites;

    public Canvas HungerBar;
    public Image[] Hunger;
    public Sprite[] HungerSprites;

    private int heartsRegenerated = 0;

    private bool timeToTake = false;

    void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;

        StartCoroutine(RegenerateHealth());
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }

        UpdateHearts();
    }

    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (currentHunger >= 80 && currentHealth < maxHealth)
            {
                TakeDamage(-5); // Regenerate 1 heart
                if(!timeToTake) {
                    timeToTake = true;
                    
                }else if(timeToTake) {
                    timeToTake = false;
                    TakeHunger(5);
                }
                
            }
        }
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

    public void TakeHunger(int hunger)
    {
        currentHunger -= hunger;
         currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);

        Debug.Log("Hunger: " + currentHunger);

        UpdateHunger();
    }

    public void UpdateHunger()
    {
        for (int i = 0; i < Hunger.Length; i++)
        {
            if (currentHunger >= (i + 1) * 10)
            {
                Hunger[i].sprite = HungerSprites[1]; // Full hunger
                Hunger[i].enabled = true;
            }
            else if (currentHunger >= (i * 10) + 5)
            {
                Hunger[i].sprite = HungerSprites[0]; // Half hunger
                Hunger[i].enabled = true;
            }
            else
            {
                Hunger[i].enabled = false; // Empty hunger
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Projectile") {
            Debug.Log("Projectile hit player");
            TakeDamage(35);
            Destroy(collision.gameObject);
        }else if (collision.gameObject.tag == "Hazard") {
            Debug.Log("Hazard hit player");
            TakeDamage(15);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "EnemyAttack") {
            TakeDamage(10);
        }else if (other.gameObject.tag == "Edible") {
            TakeHunger(-20);
            other.gameObject.SetActive(false);
        }
    }
}

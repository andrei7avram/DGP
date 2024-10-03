using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HittableStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    
    public Image HealthBar;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        HealthBar.fillAmount = currentHealth / maxHealth;
        Debug.Log(HealthBar.fillAmount);
        Debug.Log("Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

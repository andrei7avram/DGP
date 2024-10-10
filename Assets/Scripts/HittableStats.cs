using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HittableStats : MonoBehaviour
{
    public AchievementManager achievementManager;
    public float maxHealth = 100f;
    public float currentHealth;

    public Image HealthBar;

    // Static variable to ensure "First Blood" is unlocked only once
    private static bool firstBloodUnlocked = false; // Flag to track if the achievement is unlocked

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Update the health bar UI
        HealthBar.fillAmount = currentHealth / maxHealth;
        Debug.Log("Health: " + currentHealth);

        // Check if the enemy's health has reached zero
        if (currentHealth <= 0)
        {
            // Check if "First Blood" achievement is not unlocked yet
            if (!firstBloodUnlocked && achievementManager != null)
            {
                Debug.Log("Unlocking 'First Blood' achievement.");
                achievementManager.UnlockAchievement("FirstBlood"); // Unlock the achievement
                firstBloodUnlocked = true; // Mark achievement as unlocked
            }

            // Destroy the enemy game object
            Destroy(gameObject);
        }
    }
}

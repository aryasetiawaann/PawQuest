using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider; // Reference to health slider
    private Enemy enemy; // Reference to the enemy script

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();

        if (enemy == null)
        {
            Debug.LogError("Enemy component not found in parent!");
            return;
        }

        if (healthSlider == null)
        {
            Debug.LogError("Health slider not assigned in Inspector!");
            return;
        }

        // Set initial health values
        healthSlider.maxValue = enemy.maxHealth;
        healthSlider.value = enemy.currentHealth;

        // Subscribe to OnHealthChanged event
        enemy.OnHealthChanged += UpdateHealthBar;
    }

    void UpdateHealthBar(int currentHealth)
    {
        healthSlider.value = currentHealth;

        // Hide health bar when health is 0
        if (currentHealth <= 0)
        {
            healthSlider.gameObject.SetActive(false); // Hide the health bar
        }
    }

    private void OnDestroy()
    {
        if (enemy != null)
        {
            // Unsubscribe from OnHealthChanged event
            enemy.OnHealthChanged -= UpdateHealthBar;
        }
    }
}

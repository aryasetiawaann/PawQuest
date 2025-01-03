using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Sword currentSword; // Reference to the current sword

    private void Start()
    {
        // Get the Sword component attached to the Weapon GameObject
        currentSword = GetComponentInChildren<Sword>();
    }

    private void Update()
    {
        // Check if the sword has changed dynamically
        Sword newSword = GetComponentInChildren<Sword>();
        if (newSword != currentSword)
        {
            currentSword = newSword;
        }
    }

    public void EquipSword(Sword newSword)
    {
        currentSword = newSword;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HeroController heroController = GameObject.Find("Hero").GetComponent<HeroController>();
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy != null && heroController != null && heroController.isAttacking)
            {
                // Use the sword's damage value
                int damage = currentSword != null ? currentSword.damage : 0; // Fallback to 0 if sword is null
                enemy.TakeDamage(damage);
                Debug.Log("Hit!!! Damage dealt: " + damage);
            }
        }
    }
}

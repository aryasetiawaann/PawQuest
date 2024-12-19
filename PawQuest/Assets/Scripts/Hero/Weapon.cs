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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HeroAttribute hero = GameObject.Find("Hero").GetComponent<HeroAttribute>();
            HeroController heroController = GameObject.Find("Hero").GetComponent<HeroController>();
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy != null && hero != null && heroController.isAttacking)
            {
                // Use the sword's damage value
                int damage = currentSword != null ? currentSword.damage : hero.damage; // Fallback to hero's damage if sword is null
                enemy.TakeDamage(damage);
                Debug.Log("Hit!!! Damage dealt: " + damage);
            }
        }
    }
}
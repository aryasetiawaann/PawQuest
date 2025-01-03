using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Hero"){

            Enemy enemy = GetComponentInParent<Enemy>();
            HeroAttribute heroAttribute = other.GetComponent<HeroAttribute>();

            if (enemy != null && heroAttribute != null)
            {
                // Call the TakeDamage method and pass the damage value
                heroAttribute.TakeDamage(enemy.damage);
                Debug.Log("Hit by enemy" + heroAttribute.currentHealth);
            }
            else
            {
                Debug.LogWarning("HeroAttribute not found on Hero!");
            }
        }
    }

}

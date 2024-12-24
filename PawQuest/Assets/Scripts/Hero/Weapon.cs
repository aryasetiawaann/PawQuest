using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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
                int damage = hero.currentDamage;
                enemy.TakeDamage(damage);
                Debug.Log("Hit!!! Damage dealt: " + damage);
            }
        }
    }
}
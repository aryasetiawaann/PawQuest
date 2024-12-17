using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public void OnTriggerEnter(Collider  other) {
		if(other.gameObject.tag == "Enemy"){

			HeroAttribute hero = GameObject.Find("Hero").GetComponent<HeroAttribute>();
			HeroController heroController = GameObject.Find("Hero").GetComponent<HeroController>();
			Enemy enemy = other.gameObject.GetComponent<Enemy>();

			
            if (enemy != null && hero != null && heroController.isAttacking)
            {
                // Call the TakeDamage method and pass the damage value
                enemy.TakeDamage(hero.damage);
				Debug.Log("Hit!!!" + hero.damage);
            }
		}
	}
}

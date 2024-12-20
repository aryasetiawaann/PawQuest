using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttribute : MonoBehaviour
{
     // Health
	public int maxHealth = 100;
	public int damage = 20;
	public int currentHealth { get; private set; }
	public int currentDamage { get; private set; }


	// Set current health to max health
	// when starting the game.
	void Awake ()
	{
		currentHealth = maxHealth;
		currentDamage = damage;
	}

	// Damage the character
	public void TakeDamage (int getDamage)
	{
		currentHealth -= getDamage;

		// If health reaches zero
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public virtual void Die ()
	{
		// Die in some way
		// This method is meant to be overwritten
		Debug.Log(transform.name + " died.");
		Destroy(this.gameObject);
	}
}

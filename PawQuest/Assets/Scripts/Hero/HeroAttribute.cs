using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroAttribute : MonoBehaviour
{
	// Health
	public int maxHealth = 100;
	public int currentHealth { get; private set; }
	HeroController hero;
	// Set current health to max health
	// when starting the game.
	public event Action<int> OnHealthChanged;
	void Awake()
	{
		currentHealth = maxHealth;
		hero = GetComponent<HeroController>();
	}

	// Damage the character
	public void TakeDamage(int getDamage)
	{
		currentHealth -= getDamage;

		OnHealthChanged?.Invoke(currentHealth);

		// If health reaches zero
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public void Healing(int getHealth)
	{
		currentHealth += getHealth;
		if ((currentHealth + getHealth) > maxHealth)
		{
			currentHealth = 100;
		}
	}

	public virtual void Die()
	{
		// Die in some way
		// This method is meant to be overwritten
		Debug.Log(transform.name + " died.");
		hero.isDead = true;

		//End the game
	}
}

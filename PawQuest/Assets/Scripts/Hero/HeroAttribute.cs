using UnityEngine;
using System;

public class HeroAttribute : MonoBehaviour
{
	// Health
	public int maxHealth = 100;
<<<<<<< Updated upstream
	public int currentHealth { get; private set; }
	HeroController hero;
=======

	public int currentHealth { get; private set; }

	HeroController hero;

	private Sword currentSword;
>>>>>>> Stashed changes

	// Event untuk perubahan health
	public event Action<int> OnHealthChanged;

	void Awake()
	{
		currentHealth = maxHealth;
		hero = GetComponent<HeroController>();
	}

	public void TakeDamage(int getDamage)
	{
		currentHealth -= getDamage;

		// Panggil event untuk health bar
		OnHealthChanged?.Invoke(currentHealth);

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
<<<<<<< Updated upstream

		//End the game
=======
>>>>>>> Stashed changes
	}
}

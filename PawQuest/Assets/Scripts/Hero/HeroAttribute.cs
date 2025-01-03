using UnityEngine;
using System;

public class HeroAttribute : MonoBehaviour
{
	// Health
	public int maxHealth = 100;
<<<<<<< Updated upstream
	public int currentHealth { get; private set; }
<<<<<<< HEAD
	HeroController hero;
=======

	public int currentHealth { get; private set; }

	HeroController hero;

	private Sword currentSword;
>>>>>>> Stashed changes

	// Event untuk perubahan health
	public event Action<int> OnHealthChanged;

	void Awake()
=======

	// Set current health to max health
	// when starting the game.
	void Awake ()
>>>>>>> parent of 7ee8a8e (Add death animation, boss, delete openGate)
	{
		currentHealth = maxHealth;
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

<<<<<<< HEAD
	public void Healing(int getHealth)
	{
		currentHealth += getHealth;
		if ((currentHealth + getHealth) > maxHealth)
		{
			currentHealth = 100;
		}
	}

	public virtual void Die()
=======
	public virtual void Die ()
>>>>>>> parent of 7ee8a8e (Add death animation, boss, delete openGate)
	{
		// Die in some way
		// This method is meant to be overwritten
		Debug.Log(transform.name + " died.");
<<<<<<< HEAD
		hero.isDead = true;
<<<<<<< Updated upstream

		//End the game
=======
>>>>>>> Stashed changes
=======
		Destroy(this.gameObject);
>>>>>>> parent of 7ee8a8e (Add death animation, boss, delete openGate)
	}
}

using UnityEngine;
using System;

public class HeroAttribute : MonoBehaviour
{
	// Health
	public int maxHealth = 100;
	public int damage = 30;
	public int currentHealth { get; private set; }
	public int currentDamage { get; private set; }

	private Sword currentSword;

	// Event untuk perubahan health
	public event Action<int> OnHealthChanged;

	void Awake()
	{
		currentSword = GetComponentInChildren<Sword>();
		currentHealth = maxHealth;
		currentDamage = damage + currentSword.damage;
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

	public virtual void Die()
	{
		Debug.Log(transform.name + " died.");
		Destroy(this.gameObject);
	}
}

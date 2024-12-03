using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeroAttribute))]
public class HeroCombat : MonoBehaviour {

	public float attackSpeed = 1f;
	private float attackCooldown = 0f;

	public float attackDelay = .6f;

	public event System.Action OnAttack;

	HeroAttribute myStats;

	void Start ()
	{
		myStats = GetComponent<HeroAttribute>();
	}

	void Update ()
	{
		attackCooldown -= Time.deltaTime;
	}

	public void Attack (HeroAttribute targetStats)
	{
		if (attackCooldown <= 0f)
		{
			StartCoroutine(DoDamage(targetStats, attackDelay));

			if (OnAttack != null)
				OnAttack();

			attackCooldown = 1f / attackSpeed;
		}
		
	}

	IEnumerator DoDamage (HeroAttribute stats, float delay)
	{
		yield return new WaitForSeconds(delay);

		stats.TakeDamage(myStats.damage.GetValue());
	}

}
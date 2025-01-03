using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactables
{
    private HeroAttribute heroStats;
    private GameObject targetHero;

    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int damage = 2;
    public int currentHealth { get; private set; }
    public int currentDamage { get; private set; }
    [SerializeField] private float detectionRadius = 10f; // Detection range for the player
    [SerializeField] private float moveSpeed = 2f; // Speed at which the enemy moves toward the player
    [SerializeField] private float attackDistance = 4f;

    private Animator anim;
    public bool isDead = false; // Track if the enemy is dead
    private Collider enemyCollider;
    private Rigidbody rb;
    private OpenGate gate;

    // Event untuk perubahan health
    public event Action<int> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        currentDamage = damage;

        targetHero = GameObject.Find("Hero");
        heroStats = GetComponent<HeroAttribute>();
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        // Trigger event pertama untuk sinkronisasi awal health
        OnHealthChanged?.Invoke(currentHealth);
    }

    void Update()
    {
        if (isDead)
        {
            anim.SetBool("isWalk", false);
            return;
        }

        float distanceToPlayer = Vector3.Distance(targetHero.transform.position, transform.position);

        if (distanceToPlayer <= detectionRadius)
        {
            MoveTowardsPlayer();
            anim.SetBool("isWalk", true);

            if (distanceToPlayer <= attackDistance)
            {
                AttackPlayer();
                anim.SetBool("isWalk", false);
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    // Method to move the enemy towards the player
    void MoveTowardsPlayer()
    {
        if (isDead) return;

        Vector3 direction = (targetHero.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        FacePlayer();
    }

    // Method to face the player while moving
    void FacePlayer()
    {
        Vector3 direction = (targetHero.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Method to trigger attack animation
    void AttackPlayer()
    {
        anim.SetTrigger("isAttack");
    }

    // Method to handle damage taken by the enemy
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // Trigger event saat health berubah
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle enemy death
    public void Die()
    {
        if (isDead) return;

        isDead = true;
        anim.SetTrigger("isDead");

        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        anim.SetBool("isWalk", false);
        anim.ResetTrigger("isAttack");

        gate.enemyCount -= 1;

        Destroy(this.gameObject, 3f); // Adjust delay for animation timing
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floors")
        {
            gate = other.gameObject.GetComponent<OpenGate>();
        }
    }
}

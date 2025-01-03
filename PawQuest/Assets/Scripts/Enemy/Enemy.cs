using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactables
{
    private HeroAttribute heroStats;
    private GameObject targetHero;

    private bool targetDead = false;

    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int damage = 2;
    public int currentHealth { get; private set; }
    public int currentDamage { get; private set; }
    [SerializeField] private float detectionRadius = 10f; // Detection range for the player
    [SerializeField] private float moveSpeed = 2f; // Speed at which the enemy moves toward the player
    [SerializeField] private float attackDistance = 4f;

    private Animator anim; // Animator for the enemy
    public bool isDead = false; // Track if the enemy is dead
<<<<<<< Updated upstream
    private Collider enemyCollider; // Reference to the enemy's collider
    private Rigidbody rb; // Rigidbody to stop physics-based movement
    private OpenGate gate;

    // Add an AudioSource for alert sound
    [SerializeField] private AudioSource alertSound; // Assign this in the Inspector
    private bool hasPlayedAlertSound = false; // To track if alert sound has been played
=======
    private Collider enemyCollider;
    private Rigidbody rb;
>>>>>>> Stashed changes

    [SerializeField] private AudioSource alertSound; // Assign this in the Inspector
    [SerializeField] private GameObject dropPrefab; // Prefab untuk objek baru
    private bool hasPlayedAlertSound = false; // To track if alert sound has been played
    // Event untuk perubahan health
    public event Action<int> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        currentDamage = damage;
        targetHero = GameObject.Find("Hero");
        heroStats = GetComponent<HeroAttribute>();
        anim = GetComponent<Animator>(); // Get the Animator component
        enemyCollider = GetComponent<Collider>(); // Get the Collider component
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component, if any

        // Trigger event pertama untuk sinkronisasi awal health
        OnHealthChanged?.Invoke(currentHealth);
    }

    void Update()
    {
<<<<<<< HEAD
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        if (heroStats.currentHealth <= 0)
        {
            targetDead = true;
        }
=======
        // Check if the enemy is dead
>>>>>>> parent of 7ee8a8e (Add death animation, boss, delete openGate)
        if (isDead)
        {
            anim.SetBool("isWalk", false);
            return;
        }

        // Check distance to the player
        float distanceToPlayer = Vector3.Distance(targetHero.transform.position, transform.position);

<<<<<<< Updated upstream
        // If within the detection radius, move toward the player
<<<<<<< HEAD
=======
>>>>>>> Stashed changes
        if (distanceToPlayer <= detectionRadius && !targetDead)
=======
        if (distanceToPlayer <= detectionRadius)
>>>>>>> parent of 7ee8a8e (Add death animation, boss, delete openGate)
        {
            MoveTowardsPlayer();

            // Play the alert sound if not already played
            if (!hasPlayedAlertSound)
            {
                alertSound.Play();
                hasPlayedAlertSound = true; // Prevents multiple plays
            }

            // Set isWalk animation
            anim.SetBool("isWalk", true);

            // Check if within attack distance
            if (distanceToPlayer <= attackDistance)
            {
                AttackPlayer(); // Trigger attack
                anim.SetBool("isWalk", false); // Stop walking
            }
        }
        else
        {
            anim.SetBool("isWalk", false); // Stop walking if out of detection radius
            hasPlayedAlertSound = false; // Reset alert sound for next detection
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
<<<<<<< HEAD
        anim.SetTrigger("isAttack");
=======
        anim.SetTrigger("isAttack"); 
>>>>>>> parent of 7ee8a8e (Add death animation, boss, delete openGate)
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
        if (isDead) return; // Prevent multiple calls to Die

        isDead = true; // Mark as dead
        anim.SetTrigger("isDead"); // Trigger death animation

        // Disable collider
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        // Prevent all animations and interactions
        anim.SetBool("isWalk", false);
        anim.ResetTrigger("isAttack");

        gate.enemyCount -= 1;
        // Destroy the enemy after the death animation
        Destroy(this.gameObject, 3f); // Adjust delay for animation timing
<<<<<<< HEAD

        if (this.gameObject.CompareTag("Boss"))
        {
            Debug.Log("STAGE COMPLETE!!");
        }
        else if (this.gameObject.CompareTag("Enemy"))
        {
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }
    }


<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
}
=======
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.name == "Floors")
        {
            gate = other.gameObject.GetComponent<OpenGate>();
        }
    }
}
>>>>>>> parent of 7ee8a8e (Add death animation, boss, delete openGate)

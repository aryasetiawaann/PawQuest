using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles interaction with the Enemy */
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

    private Animator anim; // Animator for the enemy
    public bool isDead = false; // Track if the enemy is dead
    private Collider enemyCollider; // Reference to the enemy's collider
    private Rigidbody rb; // Rigidbody to stop physics-based movement
    private OpenGate gate;

    void Start()
    {
        currentHealth = maxHealth;
		currentDamage = damage;
        targetHero = GameObject.Find("Hero");
        heroStats = GetComponent<HeroAttribute>();
        anim = GetComponent<Animator>(); // Get the Animator component
        enemyCollider = GetComponent<Collider>(); // Get the Collider component
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component, if any
    }

void Update()
{
    // Check if the enemy is dead
    if (isDead)
    {
        anim.SetBool("isWalk", false);
        return;
    }

    // Check distance to the player
    float distanceToPlayer = Vector3.Distance(targetHero.transform.position, transform.position);

    // If within the detection radius, move toward the player
    if (distanceToPlayer <= detectionRadius)
    {
        MoveTowardsPlayer();

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
    }
}


    // Method to move the enemy towards the player
void MoveTowardsPlayer()
{
    if (isDead) return; // Do nothing if the enemy is dead

    Vector3 direction = (targetHero.transform.position - transform.position).normalized;
    transform.position += direction * moveSpeed * Time.deltaTime; // Move towards the player
    FacePlayer(); // Face the player
}


    // Method to face the player while moving
    void FacePlayer()
    {
        Vector3 direction = (targetHero.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Method to trigger attack animation and damage the player
    void AttackPlayer()
    {
        anim.SetTrigger("isAttack"); 
        
    }

    // Method to handle damage taken by the enemy
public void TakeDamage(int damage)
{
    if (isDead) return; // Ignore damage if already dead

    currentHealth -= damage;

    if (currentHealth <= 0){
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

    // Stop physics-based movement
    // if (rb != null)
    // {
    //     rb.isKinematic = true;
    //     rb.velocity = Vector3.zero;
    // }

    // Prevent all animations and interactions
    anim.SetBool("isWalk", false);
    anim.ResetTrigger("isAttack");

    gate.enemyCount -= 1;
    // Destroy the enemy after the death animation
    Destroy(this.gameObject, 3f); // Adjust delay for animation timing

    
}

private void OnTriggerEnter(Collider other) {
    if(other.gameObject.name == "Floors"){
        gate = other.gameObject.GetComponent<OpenGate>();
    }
}

}

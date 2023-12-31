using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    // This script is intended to be a superclass for enemies. Any properties that all enemies will share should be here.
    protected Rigidbody2D enemyRb;
    protected PlayerController playerScript;
    // Stats all enemies should have.
    [SerializeField] protected float health = 1.0f;
    [SerializeField] protected float speed = 2.0f;
    // Flags enemies should have.
    protected bool guarding = false;
    // Invincibility frame to prevent the same bullet from damaging twice.
    private bool vulnerable = true;
    [SerializeField] private float damageCooldown = 0.1f;
    // Start is called before the first frame update
    public void Start()
    {
        // Get the enemy's rigidbody and the player's script to be able to deal damage.
        enemyRb = GetComponent<Rigidbody2D>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    // Manages collisions with the player and the player's attacks.
    public void OnCollisionEnter2D(Collision2D collision) {
        //UnityEngine.Debug.Log("Collision Detected");
        if (collision.gameObject.CompareTag("Player")) {
            playerScript.DamagePlayer();
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //UnityEngine.Debug.Log("Trigger Detected");
        if (other.gameObject.CompareTag("PlayerAttack") && vulnerable)
        {
            // Destroy the player's bullet that hit.
            Destroy(other.gameObject);
            // Damage enemy.
            DamageEnemy();
            
        }
        // Die instantly if the death zone is collided with.
        if (other.gameObject.CompareTag("DeathZone")) {
            EnemyDeath();
        }
    }
    // Script to damage an enemy and destroy it if it runs out of health.
    public virtual void DamageEnemy() {
        if (GetGuard() == false)
        {
            health--;
            StartCoroutine(DamageCooldown());
            //UnityEngine.Debug.Log("Damaged Enemy");
            if (health <= 0)
            {
                EnemyDeath();
            }
        } else
        {
            // TODO: ADD A SOUND QUEUE TO INDICATE AN ATTACK BEING BLOCKED.

        }
    }
    public IEnumerator DamageCooldown()
    {
        vulnerable = false;
        yield return new WaitForSeconds(damageCooldown);
        vulnerable = true;
    }
    // Destroy the enemy, and trigger anything you want to happen upon death.
    public void EnemyDeath() {
        Destroy(gameObject);
    }
    // Getter and setter for guard.
    public bool GetGuard() {
        return guarding;
    }
    public void SetGuard(bool guard) { 
        guarding = guard;
    }
}

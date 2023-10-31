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
        if (collision.gameObject.CompareTag("PlayerAttack")) {
            DamageEnemy();
            // Destroy the player's bullet that hit.
            Destroy(collision.gameObject);
        }
    }
    // Script to damage an enemy and destroy it if it runs out of health.
    public void DamageEnemy() {
        if (GetGuard() == false)
        {
            health--;

            //UnityEngine.Debug.Log("Damaged Enemy");
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        } else
        {
            // TODO: ADD A SOUND QUEUE TO INDICATE AN ATTACK BEING BLOCKED.

        }
    }
    // Getter and setter for guard.
    public bool GetGuard() {
        return guarding;
    }
    public void SetGuard(bool guard) { 
        guarding = guard;
    }
}

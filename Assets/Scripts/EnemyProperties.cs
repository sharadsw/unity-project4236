using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    // This script is intended to be a superclass for enemies. Any properties that all enemies will share should be here.
    private Rigidbody2D enemyRb;
    private PlayerController playerScript;
    // Stats all enemies should have.
    [SerializeField] private float health = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Get the enemy's rigidbody and the player's script to be able to deal damage.
        enemyRb = GetComponent<Rigidbody2D>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Manages collisions with the player and the player's attacks.
    void OnCollisionEnter2D(Collision2D collision) {
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
    private void DamageEnemy() {
        health--;

        //UnityEngine.Debug.Log("Damaged Enemy");
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Crawler : EnemyProperties
{
    // Reference Variables
    public float facingDirection = -1.0f;
    
    public GameObject wallCheck;
    
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
        Walk();
    }
    // Move in correct direction.
    void Walk()
    {
        transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime * facingDirection), transform.position.y, transform.position.z);
    }
    // Turning Method for when walls are hit.
    public void Turn() {
        if (facingDirection < 0)
        {
            facingDirection = 1.0f;
        } else if (facingDirection > 0) {
            facingDirection = -1.0f;
        }
    }
    // Turn in response to colliding with other enemies.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //UnityEngine.Debug.Log("Collision Detected");
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.DamagePlayer();
        }
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            DamageEnemy();
            // Destroy the player's bullet that hit.
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Enemy") Turn();
    }
}

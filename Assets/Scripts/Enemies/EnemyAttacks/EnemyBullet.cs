using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // References to the player, the player's direction and the bullet itself.
    private GameObject player;
    public Transform bulletTransform;
    private float direction;
    protected PlayerController playerScript;
    // Stats
    public float bulletSpeed;
    public float bulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        // Assign player.
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * bulletSpeed;
    }
    // Destroy bullets when they leave the screen.
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    // Ignore collisions with the player, enemies, player and enemy bullets, and the ground. (Events still trigger, stops the pushing.)
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(8, 6);
        Physics2D.IgnoreLayerCollision(8, 7);
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(8, 10);
        // If colliding with the player, damage the player and destroy the bullet.
        if (collision.gameObject.CompareTag("Player"))
        {
            playerScript.DamagePlayer();
            Destroy(gameObject);
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        this.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
}

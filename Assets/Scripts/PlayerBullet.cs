using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // References to the player, the player's direction and the bullet itself.
    private GameObject player;
    public Transform bulletTransform;
    private float direction;
    // Stats
    public float bulletSpeed;
    public float bulletDamage;
    // Start is called before the first frame update
    void Start()
    {
        // Assign player.
        player = GameObject.Find("Player");
        // Set the direction of the bullet upon creation. -1 for left, 1 for right.
        direction = player.GetComponent<PlayerController>().facingDirection;
        // Ignore collisions with the player.
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

    }

    // Update is called once per frame
    void Update()
    {
        // The bullet will move forward until destroyed.
        bulletTransform.position = new Vector3(bulletTransform.position.x + (bulletSpeed * Time.deltaTime * direction), bulletTransform.position.y, bulletTransform.position.z);
    }
    // Destroy bullets when they leave the screen.
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
    // Ignore collisions with the player, player and enemy bullets, and the ground.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Physics2D.IgnoreLayerCollision(8, 6);
        Physics2D.IgnoreLayerCollision(8, 7);
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(8, 10);
    }
}

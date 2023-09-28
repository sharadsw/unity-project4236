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
    }

    // Update is called once per frame
    void Update()
    {
        // The bullet will move forward until destroyed.
        bulletTransform.position = new Vector3(bulletTransform.position.x + (bulletSpeed * Time.deltaTime * direction), bulletTransform.position.y, bulletTransform.position.z);
    }
}

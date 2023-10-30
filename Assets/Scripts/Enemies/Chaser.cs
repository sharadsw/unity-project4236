using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Chaser : EnemyProperties
{
    // References for the Chaser.
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the enemy's rigidbody and the player's script to be able to deal damage.
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Follow Player if the player is facing away.
        if (GetGuard() == false) FollowPlayer();
    }
    private void LateUpdate()
    {
        // Debug to see whether difference is negative.
        //UnityEngine.Debug.Log(DistanceToPlayer());
        // ModeManager will do its part at the end of every frame so it checks after player input.
        ModeManager();
    }
    // ModeManager Checks whether the player is facing the Chaser, and guards accordingly.
    void ModeManager() {
        if ((DistanceToPlayer() > 0 && playerScript.facingDirection <= 0) || (DistanceToPlayer() <= 0 && playerScript.facingDirection > 0))
        {
            SetGuard(true);
        }
        else {
            SetGuard(false);
        }
    }
    // When active, follows the player.
    void FollowPlayer() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    // Returns the horizontal distance between player and chaser. Will return negative if the player is to the left and positive if to the right.
    float DistanceToPlayer() {
        return player.transform.position.x - transform.position.x;
    }
    //

}

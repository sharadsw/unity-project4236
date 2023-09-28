using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerTransform;
    // Stat variables.
    public float walkSpeed = 5.0f;

    // Reference Variables
    public float facingDirection = 1.0f;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Walking
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            // Set a direction based on input.
            if (Input.GetKey(KeyCode.LeftArrow)) facingDirection = -1.0f;
            if (Input.GetKey(KeyCode.RightArrow)) facingDirection = 1.0f;
            // Control the walking.
            Walk();
        }
        // Jumping

        // Shooting
        if (Input.GetKey(KeyCode.C)) Shoot();
    }

    // Script to walk, with movement multiplied by either a 1 or -1 based on direction.
    void Walk() {
        playerTransform.position = new Vector3(playerTransform.position.x + (walkSpeed * Time.deltaTime * facingDirection), playerTransform.position.y, playerTransform.position.z);
    }
    // Script to shoot bullets.
    void Shoot() {
        Instantiate(bulletPrefab, playerTransform.position, bulletPrefab.transform.rotation);
    }

}

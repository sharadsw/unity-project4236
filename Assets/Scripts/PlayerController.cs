using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform playerTransform;
    // Stat variables.
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float shootCooldown = 0.5f;
    // Reference Variables
    public float facingDirection = 1.0f;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    // Status Variables
    public bool canShoot = true;
    // Recognize Ground from it being on the Ground layer.
    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Rigidbody and Box Collider
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
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
        if (Input.GetKey(KeyCode.X) && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        // Shooting
        if (Input.GetKey(KeyCode.C) && canShoot) Shoot();

    }

    // Script to walk, with movement multiplied by either a 1 or -1 based on direction.
    void Walk() {
        playerTransform.position = new Vector3(playerTransform.position.x + (walkSpeed * Time.deltaTime * facingDirection), playerTransform.position.y, playerTransform.position.z);
    }
    // Script to shoot bullets.
    void Shoot() {
        Instantiate(bulletPrefab, playerTransform.position, bulletPrefab.transform.rotation);
        StartCoroutine(ShootCooldown());
    }
    // Check whether the player is on the ground. Used to determine if the player can jump.
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    // ShootCooldown
    IEnumerator ShootCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}

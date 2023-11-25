using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject player;
    // Stat variables.
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private float damageCooldown = 1.0f;
    [SerializeField] private float maxHealth = 3.0f;
    // Reference Variables
    public float facingDirection = 1.0f;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    public TextMeshProUGUI healthText;
    // Status Variables
    public bool canShoot = true;
    public bool vulnerable = true;
    private float health;
    // Recognize Ground from it being on the Ground layer.
    [SerializeField] private LayerMask jumpableGround;
    // Scripts
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize Rigidbody and Box Collider
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        // Initialize Health.
        health = maxHealth;
        healthText.text = "Health: " + health;
        // GameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

        //playerTransform.position = new Vector3(playerTransform.position.x + (walkSpeed * Time.deltaTime * facingDirection), playerTransform.position.y, playerTransform.position.z);
        this.rb.velocity = new Vector2(this.walkSpeed * facingDirection, this.rb.velocity.y);
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
    // ShootCooldown.
    IEnumerator ShootCooldown() {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
    // Damage the player, start a damage cooldown and trigger PlayerDeath if health runs out.
    public void DamagePlayer() {
        if (vulnerable) {
            health--;
            // Update Health Text.
            healthText.text = "Health: " + health;
            if (health <= 0)
            {
                PlayerDeath();
            }
            else {
                StartCoroutine(DamageCooldown());
            }

        }
    }
    // DamageCooldown.
    IEnumerator DamageCooldown() {
        vulnerable = false;
        yield return new WaitForSeconds(damageCooldown);
        vulnerable = true;
    }
    // Deactivate the player if health runs out or by instant death, such as falling.
    public void PlayerDeath() {
        // Game Over
        gameManager.GameOver();
        // Deactivating is safer, so it is better than destroying the player object.
        player.SetActive(false);
    }
    // Heals the player based on recoverHealth, but makes sure not to go past max health.
    public void Recover(float recoverHealth) {
        if (health + recoverHealth >= maxHealth)
        {
            health = maxHealth;
        }
        else {
            health += recoverHealth;
        }
        // Update Health Text.
        healthText.text = "Health: " + health;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Die instantly if the death zone is collided with.
        if (other.gameObject.CompareTag("DeathZone"))
        {
            PlayerDeath();
        }
    }

}

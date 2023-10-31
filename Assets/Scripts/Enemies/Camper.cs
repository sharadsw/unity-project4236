using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camper : EnemyProperties
{
    [SerializeField] private float guardTime = 4.0f;
    [SerializeField] private float beforeShootTime = 2.0f;
    [SerializeField] private float afterShootTime = 2.0f;
    // References for the Camper.
    private GameObject player;
    [SerializeField] private GameObject enemyBullet;
    // Start is called before the first frame update
    void Start()
    {
        // Get the enemy's rigidbody and the player's script to be able to deal damage.
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // Start Cycle.
        StartCoroutine(Guard());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Action of shooting.
    void Shoot() {
        var heading = player.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; // This is now the normalized direction.
        Quaternion aim = Quaternion.Euler(direction.x, direction.y, direction.z);
        // Create Bullet.
        Instantiate(enemyBullet, transform.position, aim);
    }
    // Action Cycle. Cycles between Guard, BeforeShoot and AfterShoot.
    // Guard.
    IEnumerator Guard()
    {
        UnityEngine.Debug.Log("Guard");
        SetGuard(true);
        yield return new WaitForSeconds(guardTime);
        SetGuard(false);

        // Start BeforeShoot.
        StartCoroutine(BeforeShoot());
    }
    // Two vulnerable states.
    IEnumerator BeforeShoot()
    {
        UnityEngine.Debug.Log("Vulnerable");
        yield return new WaitForSeconds(beforeShootTime);
        // Shoot now.
        Shoot();
        // Start AfterShoot.
        StartCoroutine(AfterShoot());
    }
    IEnumerator AfterShoot()
    {
        
        yield return new WaitForSeconds(afterShootTime);
        // Start Guard.
        StartCoroutine(Guard());
    }
}

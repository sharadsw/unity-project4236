using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class SnakeBoss : EnemyProperties
{
    // Directional bounds.
    [SerializeField] private float leftBound = -19.5f;
    [SerializeField] private float rightBound = 19.5f;
    [SerializeField] private float upperBound = 8.5f;
    [SerializeField] private float lowerBound = -8.5f;
    // Movement Cycle.
    [SerializeField] private float moveCycle = 0.1f;
    // Planned Direction
    // Left is 0, Right is 1, Up is 2, Down is 3.
    private int directionState = 0;
    // Target Coordinates.
    private Vector3 targetPos;
    // Player Reference.
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // Get the enemy's rigidbody and the player's script to be able to deal damage.
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // Remaining setup.
        
        // Begin Cycle.
        StartCoroutine(MoveCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Move()
    {
        // If the target has been reached, calculate target and set direction state.
        
        // Using a switch to determine direction. Continues along the path until the target is reached or a wall is in sight.
        switch (directionState) { 
        // Left
        case 0:
                if (transform.position.x == leftBound) {
                    EmergencyTurn();
                } else {
                    transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                }
            break;
        // Right
        case 1:
                if (transform.position.x == rightBound)
                {
                    EmergencyTurn();
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                }
            break;
        // Up
        case 2:
                if (transform.position.y == upperBound)
                {
                    EmergencyTurn();
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                }
            break;
        // Down
        case 3:
                if (transform.position.y == lowerBound)
                {
                    EmergencyTurn();
                } else { 
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                }
            break;
        }
        
    }
    // Movement Cycle.
    IEnumerator MoveCycle() {
        Move();
    yield return new WaitForSeconds(moveCycle);
        StartCoroutine(MoveCycle());
    }
    // Change Direction
    private void SetDirectionState(int direction) {
        directionState = direction;
    }
    // Emergency Turn. Reflex that activates when at the border of the map.
    private void EmergencyTurn() {
        switch (directionState)
        {
            // Left
            case 0:
                if (transform.position.y == upperBound)
                {
                    SetDirectionState(3);
                }
                else if (transform.position.y == lowerBound) {
                    SetDirectionState(2);
                } else {
                    if (player.transform.position.y - transform.position.y > 0)
                    {
                        SetDirectionState(2);
                    }
                    else {
                        SetDirectionState(3);
                    }
                }
                break;
            // Right
            case 1:
                if (transform.position.y == upperBound)
                {
                    SetDirectionState(3);
                }
                else if (transform.position.y == lowerBound)
                {
                    SetDirectionState(2);
                }
                else
                {
                    if (player.transform.position.y - transform.position.y > 0)
                    {
                        SetDirectionState(2);
                    }
                    else
                    {
                        SetDirectionState(3);
                    }
                }
                break;
            // Up
            case 2:
                if (transform.position.x == leftBound)
                {
                    SetDirectionState(1);
                }
                else if (transform.position.x == rightBound)
                {
                    SetDirectionState(0);
                }
                else
                {
                    if (player.transform.position.x - transform.position.x > 0)
                    {
                        SetDirectionState(1);
                    }
                    else
                    {
                        SetDirectionState(0);
                    }
                }
                break;
            // Down
            case 3:
                if (transform.position.x == leftBound)
                {
                    SetDirectionState(1);
                }
                else if (transform.position.x == rightBound)
                {
                    SetDirectionState(0);
                }
                else
                {
                    if (player.transform.position.x - transform.position.x > 0)
                    {
                        SetDirectionState(1);
                    }
                    else
                    {
                        SetDirectionState(0);
                    }
                }
                break;
        }
        switch (directionState) {
            case 0:
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                break;
        }
        UnityEngine.Debug.Log("Emergency Turn.");
    }
    // Calculate Target. Big Brain Time.
    private void CalculateTarget() {
        // For player position, round to whole number, then if positive, subtract 0.5, and if negative, add 0.5.
        
    }
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
        
    }
}

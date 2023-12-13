using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SnakeBoss : EnemyProperties
{
    // Directional bounds.
    [SerializeField] private float leftBound = -19.5f;
    [SerializeField] private float rightBound = 19.5f;
    [SerializeField] private float upperBound = 8.5f;
    [SerializeField] private float lowerBound = -8.5f;
    // Movement Cycle.
    [SerializeField] private float moveCycle = 0.1f;
    // Bullet Cycle.
    [SerializeField] private float bulletCycle = 4.0f;
    // Planned Direction
    // Left is 0, Right is 1, Up is 2, Down is 3.
    private int directionState = 0;
    // Target Coordinates.
    private Vector3 targetPos;
    // Player Reference.
    private GameObject player;
    // Arrived Flag.
    public bool arrived = false;
    // Checks for Horizontal or vertical movement. True means Horizontal, False means Vertical.
    public bool horizontalOrVertical = true;
    // Segment Reference.
    public GameObject segment1;
    public GameObject segment2;
    public GameObject segment3;
    // Bullet Reference.
    public GameObject bulletPrefab;
    // UI.
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        // Get the enemy's rigidbody and the player's script to be able to deal damage.
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // Remaining setup.
        // Set arrived as true when testing a path on start.
        arrived = true;
        // UI.
        // Initialize UI on Start
        healthText.gameObject.SetActive(true);
        healthText.text = "Boss: " + health;
        // Begin Cycles.
        StartCoroutine(MoveCycle());
        StartCoroutine(BulletCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Move()
    {
        // If the target has been reached, calculate a new target and set direction state.
        if (arrived) {
            CalculateTarget();
        }
        // Segment 3 takes Segment 2's former position.
        segment3.transform.position = segment2.transform.position;
        // Segment 2 takes Segment 1's former position.
        segment2.transform.position = segment1.transform.position;
        // Segment 1 takes the player's former position.
        segment1.transform.position = transform.position;
        // Using a switch to determine direction. Continues along the path until the target is reached or a wall is in sight.
        switch (directionState) { 
        // Left
        case 0:
                if (transform.position.x == leftBound)
                {
                    EmergencyTurn();
                }
                else if (transform.position.x == 14.5f && transform.position.y == -4.5f) {
                    EmergencyTurn();
                }
                else if (transform.position.x == -5.5f && transform.position.y == -4.5f)
                {
                    EmergencyTurn();
                }
                else if (transform.position.x == 4.5f && transform.position.y == 0.5f)
                {
                    EmergencyTurn();
                }
                else
                {
                    transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                }
            break;
        // Right
        case 1:
                if (transform.position.x == rightBound)
                {
                    EmergencyTurn();
                }
                else if (transform.position.x == -14.5f && transform.position.y == -4.5f)
                {
                    EmergencyTurn();
                }
                else if (transform.position.x == 5.5f && transform.position.y == -4.5f)
                {
                    EmergencyTurn();
                }
                else if (transform.position.x == -4.5f && transform.position.y == 0.5f)
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
                else if (transform.position.x <= 3.5f && transform.position.x >= -3.5f && transform.position.y == -0.5f)
                {
                    EmergencyTurn();
                }
                else if (transform.position.x <= -6.5f && transform.position.x >= -13.5f && transform.position.y == -5.5f)
                {
                    EmergencyTurn();
                }
                else if (transform.position.x <= 13.5f && transform.position.x >= 6.5f && transform.position.y == -5.5f)
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
                }
                else if (transform.position.x <= 3.5f && transform.position.x >= -3.5f && transform.position.y == 1.5f)
                {
                    EmergencyTurn();
                }
                else if (transform.position.x <= -6.5f && transform.position.x >= -13.5f && transform.position.y == -3.5f)
                {
                    EmergencyTurn();
                }
                else if (transform.position.x <= 13.5f && transform.position.x >= 6.5f && transform.position.y == -3.5f)
                {
                    EmergencyTurn();
                }
                else { 
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                }
            break;
        }
        // Check if arrived can be marked as true.
        if (horizontalOrVertical)
        {
            if (transform.position.x+1 > targetPos.x && transform.position.x - 1 < targetPos.x) arrived = true;
        }
        else {
            if (transform.position.y + 1 > targetPos.y && transform.position.y - 1 < targetPos.y) arrived = true;
        }
        if (arrived) UnityEngine.Debug.Log("Arrived.");
    }
    // Action of shooting.
    void Shoot()
    {
        var heading = player.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; // This is now the normalized direction.
        Quaternion aim = Quaternion.Euler(direction.x, direction.y, direction.z);
        // Create Bullet.
        Instantiate(bulletPrefab, transform.position, aim);
    }
    // Frequency that bullets spawn at.
    IEnumerator BulletCycle() {
        yield return new WaitForSeconds(bulletCycle);
        Shoot();
        if (player.activeInHierarchy) StartCoroutine(BulletCycle());
    }
    // Movement Cycle.
    IEnumerator MoveCycle() {
        Move();
    yield return new WaitForSeconds(moveCycle);
        // The cycle continues until either the player dies, or the scene ends with the boss dying.
        if(player.activeInHierarchy) StartCoroutine(MoveCycle());
    }
    // Change Direction. Left is 0, Right is 1, Up is 2, Down is 3.
    private void SetDirectionState(int direction) {
        directionState = direction;
    }
    // Emergency Turn. Reflex that activates when at the border of the map. End with calculating the target.
    private void EmergencyTurn() {
        bool throughFloatingPlatform = false;
        switch (directionState)
        {
            // Left
            case 0:
                // Small Platform exception case.
                if (transform.position.x == 14.5f && transform.position.y == -4.5f)
                {
                    if (player.transform.position.y - transform.position.y > 0)
                    {
                        SetDirectionState(2);
                    }
                    else
                    {
                        SetDirectionState(3);
                    }
                    break;
                }
                else if (transform.position.x == -5.5f && transform.position.y == -4.5f)
                {
                    if (player.transform.position.y - transform.position.y > 0)
                    {
                        SetDirectionState(2);
                    }
                    else
                    {
                        SetDirectionState(3);
                    }
                    break;
                }
                else if (transform.position.x == 4.5f && transform.position.y == 0.5f)
                {
                    if (player.transform.position.y - transform.position.y > 0)
                    {
                        SetDirectionState(2);
                    }
                    else
                    {
                        SetDirectionState(3);
                    }
                    break;
                }
                // Side of map.
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
                // Small Platform exception case.
                if (transform.position.x == -14.5f && transform.position.y == -4.5f)
                {
                    if (player.transform.position.y - transform.position.y > 0)
                    {
                        SetDirectionState(2);
                    }
                    else
                    {
                        SetDirectionState(3);
                    }
                    break;
                }
                else if (transform.position.x == 5.5f && transform.position.y == -4.5f)
                {
                    if (player.transform.position.y - transform.position.y > 0)
                    {
                        SetDirectionState(2);
                    }
                    else
                    {
                        SetDirectionState(3);
                    }
                    break;
                }
                else if (transform.position.x == -4.5f && transform.position.y == 0.5f)
                {
                    if (player.transform.position.y - transform.position.y > 0)
                    {
                        SetDirectionState(2);
                    }
                    else
                    {
                        SetDirectionState(3);
                    }
                    break;
                }
                // Side of map.
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
                // Small Platform exception case.
                if (transform.position.x <= 3.5f && transform.position.x >= -3.5f && transform.position.y == -0.5f)
                {
                    if (player.transform.position.x - transform.position.x > 0)
                    {
                        SetDirectionState(1);
                        targetPos = new Vector3(4.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    else
                    {
                        SetDirectionState(0);
                        targetPos = new Vector3(-4.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    break;
                }
                else if (transform.position.x <= -6.5f && transform.position.x >= -13.5f && transform.position.y == -5.5f)
                {
                    if (player.transform.position.x - transform.position.x > 0)
                    {
                        SetDirectionState(1);
                        targetPos = new Vector3(-5.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    else
                    {
                        SetDirectionState(0);
                        targetPos = new Vector3(-14.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    break;
                }
                else if (transform.position.x <= 13.5f && transform.position.x >= 6.5f && transform.position.y == -5.5f)
                {
                    if (player.transform.position.x - transform.position.x > 0)
                    {
                        SetDirectionState(1);
                        targetPos = new Vector3(14.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    else
                    {
                        SetDirectionState(0);
                        targetPos = new Vector3(5.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    break;
                }
                // Side of map.
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
                // Small Platform exception case.
                if (transform.position.x <= 3.5f && transform.position.x >= -3.5f && transform.position.y == 1.5f)
                {
                    if (player.transform.position.x - transform.position.x > 0)
                    {
                        SetDirectionState(1);
                        targetPos = new Vector3(4.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    else
                    {
                        SetDirectionState(0);
                        targetPos = new Vector3(-4.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    break;
                }
                else if (transform.position.x <= -6.5f && transform.position.x >= -13.5f && transform.position.y == -3.5f)
                {
                    if (player.transform.position.x - transform.position.x > 0)
                    {
                        SetDirectionState(1);
                        targetPos = new Vector3(-5.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    else
                    {
                        SetDirectionState(0);
                        targetPos = new Vector3(-14.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    break;
                }
                else if (transform.position.x <= 13.5f && transform.position.x >= 6.5f && transform.position.y == -3.5f)
                {
                    if (player.transform.position.x - transform.position.x > 0)
                    {
                        SetDirectionState(1);
                        targetPos = new Vector3(14.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    else
                    {
                        SetDirectionState(0);
                        targetPos = new Vector3(5.5f, transform.position.y, transform.position.z);
                        throughFloatingPlatform = true;
                    }
                    break;
                }
                // Side of map.
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
        if (throughFloatingPlatform)
        {
            horizontalOrVertical = true;
            arrived = false;
        }
        if(!throughFloatingPlatform) CalculateTarget();
    }
    
    // Determine Direction based on location of targetPos.
    private void ChooseDirection() {
        float bossX = transform.position.x;
        float bossY = transform.position.y;
        float targetX = targetPos.x;
        float targetY = targetPos.y;
        // Find direction based on distance and whether it would be positive or negative without absolute value.
        
        float xDistance = targetX - bossX;
        float yDistance = targetY - bossY;
        // Old if statement.
        //if (Mathf.Abs(xDistance) <= Mathf.Abs(yDistance))
        if(directionState == 2 || directionState == 3)
        {
            horizontalOrVertical = true;
            if (xDistance> 0)
            {
                SetDirectionState(1);
            }
            else {
                SetDirectionState(0);
            }
        }
        else {
            horizontalOrVertical = false;
            if (yDistance > 0)
            {
                SetDirectionState(2);
            }
            else
            {
                SetDirectionState(3);
            }
        }
        //UnityEngine.Debug.Log("X Distance: " + xDistance + " Y Distance: " + yDistance);
    }
    // Calculate Target.
    private void CalculateTarget() {
        // For player position, round to whole number, then if positive, subtract 0.5, and if negative, add 0.5.
        // Initialize Coordinates.
        float targetX = 0;
        float targetY = 0;
        float targetZ = 0;
        bool isPlayer = true;
        

        if (isPlayer)
        {
            // Apply coordinates to player position.
            targetX = player.transform.position.x;
            targetY = player.transform.position.y;
            // Coordinate cleanup.
            targetX = Mathf.Round(targetX);
            targetY = Mathf.Round(targetY);
        }
        if (targetX < 0)
        {
            // ONCE THE PLAYER IS PROPERLY SIZED, THIS MAY NEED TO CHANGE.
            targetX -= 0.5f;
        }
        else {
            targetX -= 0.5f;
        }
        if (targetY < 0)
        {
            targetY += 0.5f;
        }
        else {
            targetY -= 0.5f;
        }
        //UnityEngine.Debug.Log(targetX + " " + targetY);
        // Set targetPos to coordinates.
        targetPos = new Vector3(targetX, targetY, targetZ);
        ChooseDirection();
        arrived = false;
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        //UnityEngine.Debug.Log("Trigger Detected");
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            // Destroy the player's bullet that hit.
            Destroy(other.gameObject);
            // Damage enemy.
            DamageEnemy();
            
        }
        // Die instantly if the death zone is collided with.
        if (other.gameObject.CompareTag("DeathZone"))
        {
            EnemyDeath();
        }
    }
    public override void DamageEnemy()
    {
        if (GetGuard() == false)
        {
            health--;
            // Update UI.
            healthText.text = "Boss: " + health;
            StartCoroutine(DamageCooldown());
            //UnityEngine.Debug.Log("Damaged Boss");
            if (health <= 0)
            {
                SceneManager.LoadScene("WinScene");
            }
        }
        else
        {
            

        }
    }
    
}

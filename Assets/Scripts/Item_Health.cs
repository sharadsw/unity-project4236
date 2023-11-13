using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Health : MonoBehaviour
{
    // Reference to player script.
    protected PlayerController playerScript;
    [SerializeField] private float healAmount = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Find player Script.
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Player Collision
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript.Recover(healAmount);
            Destroy(gameObject);
        }
    }
}

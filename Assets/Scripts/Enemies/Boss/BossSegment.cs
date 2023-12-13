using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSegment : MonoBehaviour
{
    protected PlayerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript.DamagePlayer();
            
        }
    }
}

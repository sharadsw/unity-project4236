using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallCheck : MonoBehaviour
{
    public bool wallDetection = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) {
            SetWallCheck(false);
            Debug.Log("Wall Collision Start");
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Wall Collision End");
            SetWallCheck(true);
        }
    }
    // Getter
    public bool GetWallCheck() { 
    return wallDetection;
    }
    // Setter
    public void SetWallCheck(bool wallCheck)
    {
        wallDetection = wallCheck;
    }
}

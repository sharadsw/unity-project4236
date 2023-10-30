using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckHitbox : MonoBehaviour
{
    // Get parent object.
    private GameObject parent;
    private Crawler parentScript;
    private bool cooldown = false;


    // Start is called before the first frame update
    void Start()
    {

        // Gets parent.
        parent = this.transform.parent.gameObject;
        parentScript = parent.GetComponent<Crawler>();
        UnityEngine.Debug.Log("a");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !cooldown) {
            parentScript.Turn();
            StartCoroutine(WallCooldown());
            UnityEngine.Debug.Log("HIT");
        }
    }
    IEnumerator WallCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }
}

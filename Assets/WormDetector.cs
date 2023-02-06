using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormDetector : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Worm Barrier instantiated");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something hit barrier");
        if(collision.gameObject.CompareTag("BGEnemy"))
        {
            Debug.Log("Worm hit barrier");
            collision.gameObject.GetComponent<WormController>().turnWorm();
        }
    }

}

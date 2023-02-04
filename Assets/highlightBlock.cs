using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlightBlock : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    
    public bool hasTower = false;

    private Color blockColor;
    
    public GameObject tower1;

    void Start()
    {
        // http://answers.unity.com/answers/993502/view.html

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        blockColor = gameObject.GetComponent<SpriteRenderer>().color;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("character") && !hasTower)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(blockColor.r, blockColor.g, blockColor.b, .5f);
            
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector3 towerPos = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f);
            Instantiate(tower1, towerPos, Quaternion.identity, null);
            Debug.Log("PLACE TOWER");
            hasTower = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(blockColor.r, blockColor.g, blockColor.b, 1f);
        }
    }
}

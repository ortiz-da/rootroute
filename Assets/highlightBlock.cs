using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class highlightBlock : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    public Tilemap tilemap;

    public bool hasTower = false;

    private Color blockColor;
    
    public GameObject tower1;

    private ResourceManager resourceManager;

    public GameObject mycelium;

    public TileBase mineshaftWithMyceliumTile;

    void Start()
    {
        // http://answers.unity.com/answers/993502/view.html
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("character") && !hasTower)
        {
            Debug.Log("ENTER");

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            blockColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(blockColor.r, blockColor.g, blockColor.b, .5f);
            
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (Input.GetKeyDown(KeyCode.F) && !hasTower)
        {
            Vector3 towerPos = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f);
            Instantiate(tower1, towerPos, Quaternion.identity, null);

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;
            Vector3Int buildBlock = tilemap.WorldToCell(transform.position);
            tilemap.SetTile(buildBlock, mineshaftWithMyceliumTile);

            Debug.Log("PLACE TOWER");
            hasTower = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(blockColor.r, blockColor.g, blockColor.b, 1f);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("character"))
        {
            Debug.Log("LEAVE");

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            blockColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(blockColor.r, blockColor.g, blockColor.b, 1f);        }
    }
}

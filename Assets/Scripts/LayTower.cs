using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayTower : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject tower1;

    private ResourceManager resourceManager;

    private TextMeshProUGUI errorText;

    private Collider2D grassTrigger;

    private highlightBlock currentGrass;
    void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();

        errorText = GameObject.Find("errorText").GetComponent<TextMeshProUGUI>();

        grassTrigger = gameObject.GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && currentGrass != null)
        {
            if(!currentGrass.hasTower)
            {
                currentGrass.placeTower();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("grass"))
        {
            currentGrass = collision.gameObject.GetComponent<highlightBlock>();
        }
        else
            currentGrass= null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentGrass = null;
    }


}

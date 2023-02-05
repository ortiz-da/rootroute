using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bioResource : MonoBehaviour
{
    public float resourceRate = 1f;
    public float resourceProviding = 1f;
    public Vector3Int position;
    public Vector2Int correctedPosition;
    public Tilemap tilemap;

    private ResourceManager resourceManager;

    public bool connected = false;
    private bool sending = false;
    void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        position = tilemap.WorldToCell(transform.position);
        correctedPosition = new Vector2Int();
    }

    // Update is called once per frame
    void Update()
    {
        if(connected && sending)
        {
            StartCoroutine(sendBiomass());
        }
    }

    IEnumerator sendBiomass()
    {
        resourceManager.biomassUpdate(resourceProviding);
        sending = false;
        yield return new WaitForSeconds(resourceRate);
        sending = true;
    }
}

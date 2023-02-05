using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bioResource : MonoBehaviour
{
    public float resourceRate = 2f;
    public float resourceProviding = 1f;
    public Vector3Int position;
    public Tilemap tilemap;

    public ResourceManager resourceManager;

    public bool connected = false;
    private bool sending = false;
    void Start()
    {
        position = tilemap.WorldToCell(transform.position);
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
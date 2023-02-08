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

    private float timeLeft;
    void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        position = tilemap.WorldToCell(transform.position);
        correctedPosition = new Vector2Int();

        timeLeft = VariableSetup.biomassLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (connected && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            // Debug.Log(timeLeft.ToString());
        }
        if (connected && sending)
        {
            StartCoroutine(sendBiomass());
        }

        if(timeLeft <= 0)
        {

            // TODO: can't get it to stop sending resources
            gameObject.SetActive(false);

            
            /*
             *             StopAllCoroutines();
            connected = false;
            sending = false;
            Destroy(gameObject);
            resourceManager.biomassRate -= resourceProviding;
             */

        }
    }

    IEnumerator sendBiomass()
    {
        Debug.Log("SENDING");
        resourceManager.biomassUpdate(resourceProviding);
        sending = false;
        yield return new WaitForSeconds(resourceRate);
        sending = true;
    }
}

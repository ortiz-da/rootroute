using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BioResource : MonoBehaviour
{
    public float resourceRate = 1f;
    public float resourceProviding = 1f;
    public Vector3Int position;
    public Vector2Int correctedPosition;
    public Tilemap tilemap;

    public bool connected;

    private ResourceManager _resourceManager;
    private bool _sending;

    private float _timeLeft;

    private void Start()
    {
        _resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        position = tilemap.WorldToCell(transform.position);
        correctedPosition = new Vector2Int();

        _timeLeft = VariableSetup.biomassLife;
    }

    // Update is called once per frame
    private void Update()
    {
        if (connected && _timeLeft > 0) _timeLeft -= Time.deltaTime;
        // Debug.Log(timeLeft.ToString());
        if (connected && _sending) StartCoroutine(SendBiomass());

        // possum disappears after being used for a bit
        if (_timeLeft <= 0)
        {
            // TODO: can't get it to stop sending resources
            // gameObject.SetActive(false);
            //resourceManager.removeResourc(gameObject);
            /*Debug.Log("resource depleted");
            StopAllCoroutines();
            Destroy(gameObject);*/
            /*StopAllCoroutines();
            connected = false;
            sending = false;
            this.transform.position = new Vector3(Random.Range(0f, VariableSetup.worldXSize),
                Random.Range(0f, VariableSetup.worldYSize), 0);
            timeLeft = VariableSetup.biomassLife;*/
        }
    }

    private IEnumerator SendBiomass()
    {
        Debug.Log("SENDING");
        // resourceManager.biomassUpdate(resourceProviding);
        _sending = false;
        yield return new WaitForSeconds(resourceRate);
        _sending = true;
    }

    public void ConnectResource()
    {
        connected = true;
        gameObject.GetComponent<Animator>().SetBool("PossumConnected", true);
    }

    public void DisconnectResource()
    {
        connected = false;
        gameObject.GetComponent<Animator>().SetBool("PossumConnected", false);
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bioResource : MonoBehaviour
{
    public float resourceRate = 1f;
    public float resourceProviding = 1f;
    public Vector3Int position;
    public Vector2Int correctedPosition;
    public Tilemap tilemap;

    public bool connected;

    private ResourceManager resourceManager;
    private bool sending;

    private float timeLeft;

    private void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        position = tilemap.WorldToCell(transform.position);
        correctedPosition = new Vector2Int();

        timeLeft = VariableSetup.biomassLife;
    }

    // Update is called once per frame
    private void Update()
    {
        if (connected && timeLeft > 0) timeLeft -= Time.deltaTime;
        // Debug.Log(timeLeft.ToString());
        if (connected && sending) StartCoroutine(sendBiomass());

        // possum disappears after being used for a bit
        if (timeLeft <= 0)
        {
            // TODO: can't get it to stop sending resources
            // gameObject.SetActive(false);
            //resourceManager.removeResourc(gameObject);
            Debug.Log("resource depleted");
            StopAllCoroutines();
            Destroy(gameObject);
            /*StopAllCoroutines();
            connected = false;
            sending = false;
            this.transform.position = new Vector3(Random.Range(0f, VariableSetup.worldXSize),
                Random.Range(0f, VariableSetup.worldYSize), 0);
            timeLeft = VariableSetup.biomassLife;*/
        }
    }

    private IEnumerator sendBiomass()
    {
        Debug.Log("SENDING");
        // resourceManager.biomassUpdate(resourceProviding);
        sending = false;
        yield return new WaitForSeconds(resourceRate);
        sending = true;
    }

    public void connectResource()
    {
        connected = true;
        gameObject.GetComponent<Animator>().SetBool("PossumConnected", true);
    }

    public void disconnectResource()
    {
        connected = false;
        gameObject.GetComponent<Animator>().SetBool("PossumConnected", false);
    }
}
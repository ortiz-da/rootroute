using UnityEngine;
using UnityEngine.Tilemaps;

public class WormController : MonoBehaviour
{
    private readonly float wormSpeed = 2f;


    private Vector3 wormDestination;


    private Tilemap _tilemap;

    public TileBase mineshaftTile;
    private ResourceManager resourceManager;



    private void Start()
    {

        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        _tilemap = FindObjectOfType<Tilemap>();
        wormDestination = new Vector3(Random.Range(-15f, 15f), Random.Range(-20f, 2f), 0);


        // https://answers.unity.com/questions/1023987/lookat-only-on-z-axis.html
        var difference = wormDestination - transform.position;
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        // transform.Rotate(0, 0, Random.Range(1, 360));
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3Int worldCell = _tilemap.WorldToCell(transform.position);
        
        // Debug.Log(_tilemap.GetTile(worldCell).name);

        // If worm comes in contact with mycelium, destroy the mycelium.
        if (_tilemap.GetTile(worldCell).name.Equals("MyceliumRuleTile"))
        {
            _tilemap.SetTile(worldCell, mineshaftTile);
            resourceManager.myceliumDeleted(worldCell); // possibly buggy?
        }
        

        var step = wormSpeed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector3.MoveTowards(transform.position, wormDestination, step);

        if (transform.position.Equals(wormDestination))
        {
            wormDestination = new Vector3(Random.Range(-15f, 15f), Random.Range(-20f, 2f));

            // https://answers.unity.com/questions/1023987/lookat-only-on-z-axis.html
            var difference = wormDestination - transform.position;
            var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }
}
using UnityEngine;
using UnityEngine.Tilemaps;

public class WormController : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase mineshaftTile;
    private readonly float wormSpeed = 2f;
    private ResourceManager resourceManager;


    private Vector3 wormDestination;


    private void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        wormDestination = new Vector3(Random.Range(0f, VariableSetup.worldXSize),
            Random.Range(0f, VariableSetup.worldYSize), 0);


        // https://answers.unity.com/questions/1023987/lookat-only-on-z-axis.html
        var difference = wormDestination - transform.position;
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        // transform.Rotate(0, 0, Random.Range(1, 360));
    }

    // Update is called once per frame
    private void Update()
    {
        var worldCell = tilemap.WorldToCell(transform.position);

        // Debug.Log(_tilemap.GetTile(worldCell).name);

        // If worm comes in contact with mycelium, destroy the mycelium.
        if (tilemap.GetTile(worldCell).name.Equals("MyceliumRuleTile"))
        {
            tilemap.SetTile(worldCell, mineshaftTile);
            resourceManager.myceliumDeleted(worldCell); // possibly buggy?
        }


        var step = wormSpeed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector3.MoveTowards(transform.position, wormDestination, step);

        if (transform.position.Equals(wormDestination))
        {
            wormDestination = new Vector3(Random.Range(0f, VariableSetup.worldXSize),
                Random.Range(0f, VariableSetup.worldYSize), 0);

            // https://answers.unity.com/questions/1023987/lookat-only-on-z-axis.html
            var difference = wormDestination - transform.position;
            var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }
}
using UnityEngine;
using UnityEngine.Tilemaps;

public class WormController : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase mineshaftTile;
    private readonly float _wormSpeed = 2f;
    private ResourceManager _resourceManager;


    private Vector3 _wormDestination;

    // the origin block
    private int _originX;
    private int _originY;


    private void Start()
    {
        tilemap = GameObject.Find("Grid").transform.GetChild(0).gameObject.GetComponent<Tilemap>();


        var size = tilemap.size;
        _originX = size.x / 2;
        _originY = size.y - 1;

        // todo fix
        _resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        _wormDestination = new Vector3(Random.Range(0f, VariableSetup.worldXSize),
            Random.Range(0f, VariableSetup.worldYSize), 0);


        // https://answers.unity.com/questions/1023987/lookat-only-on-z-axis.html
        var difference = _wormDestination - transform.position;
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
        TileBase tile = tilemap.GetTile(worldCell);
        // dont allow eating of origin block
        if (tile != null && tile.name.Equals("MyceliumRuleTile") &&
            !(worldCell.x == _originX && worldCell.y == _originY))
        {
            tilemap.SetTile(worldCell, mineshaftTile);
            _resourceManager.MyceliumDeleted(worldCell); // possibly buggy?
        }


        var step = _wormSpeed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector3.MoveTowards(transform.position, _wormDestination, step);

        if (transform.position.Equals(_wormDestination))
        {
            _wormDestination = new Vector3(Random.Range(0f, VariableSetup.worldXSize),
                Random.Range(0f, VariableSetup.worldYSize), 0);

            // https://answers.unity.com/questions/1023987/lookat-only-on-z-axis.html
            var difference = _wormDestination - transform.position;
            var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }
}
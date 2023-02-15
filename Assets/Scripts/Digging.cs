using UnityEngine;
using UnityEngine.Tilemaps;

public class Digging : MonoBehaviour
{
    public Tilemap tilemap;

    public Camera camera1;

    public Animator animator;

    public TileBase mineshaftTile;

    public TileBase mineshaftWithMyceliumTile;

    public ResourceManager resourceManager;

    public AudioClip digSound;
    public AudioClip placeSound;

    public Tilemap backgroundTilemap;

    private readonly float _mineDistance = 1.3f;
    private readonly float _placeDistance = 2f;


    private AudioSource _audioSource;

    // TODO name these better
    private Vector3Int _clickedBlock;
    private Vector3 selectedPoint;

    // the origin block
    private int _originX;
    private int _originY;

    private static readonly int Mining = Animator.StringToHash("Mining");
    private static readonly int Building = Animator.StringToHash("Building");


    // Start is called before the first frame update
    private void Start()
    {
        var size = tilemap.size;
        _originX = size.x / 2;
        _originY = size.y - 1;
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            // https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/
            // https://stackoverflow.com/a/56519572
            selectedPoint = camera1.ScreenToWorldPoint(Input.mousePosition);
            selectedPoint = new Vector3(selectedPoint.x, selectedPoint.y, 0);
            var distance = Vector3.Distance(selectedPoint, transform.position);

            _clickedBlock = tilemap.WorldToCell(selectedPoint);

            // Left click to mine blocks
            // Only mine blocks if the player is close enough and below ground
            if (Input.GetButton("Fire1") && distance <= _mineDistance && InBounds(selectedPoint))
            {
                animator.SetBool(Mining, true);
            }

            // Right click to place mycelium.
            else if (Input.GetButton("Fire2") && distance <= _placeDistance && InBounds(selectedPoint))
            {
                animator.SetBool(Building, true);
            }
        }
    }

    // checks if a point is within tilemap
    private static bool InBounds(Vector3 selectedPoint)
    {
        bool result = selectedPoint.y < VariableSetup.worldYSize && selectedPoint.y >= 0 && selectedPoint.x >= 0 &&
                      selectedPoint.x < VariableSetup.worldXSize;
        // Debug.Log(result);
        return result;
    }

    // Deletes a block that a player mined. Is called by an animation event that occurs on the last frame of the mine animation.
    public void DeleteBlock()
    {
        animator.SetBool(Mining, false);

        TileBase tile = tilemap.GetTile(_clickedBlock);

        // Debug.Log(_clickedBlock);

        // Don't allow players to mine air, worldborder tiles, or origin block
        // Need to also check in bounds here, in case the player quickly moves their mouse while holding after the initial click?
        if (tile != null && !tile.name.Equals("WorldBorder1") &&
            !(_clickedBlock.x == _originX && _clickedBlock.y == _originY && InBounds(selectedPoint))
           )
        {
            var clickedBlockName = tilemap.GetTile(_clickedBlock).name;
            backgroundTilemap.SetTile(_clickedBlock, mineshaftTile);
            tilemap.SetTile(_clickedBlock, null);

            // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
            _audioSource.clip = digSound;
            _audioSource.pitch = Random.Range(.5f, 1f);
            _audioSource.Play();

            // Code for when player mines a mycelium tile.
            if (clickedBlockName.Equals("MyceliumRuleTile"))
                resourceManager.myceliumDeleted(_clickedBlock);
        }
    }


    // Places a mycelium block. Is called by an animation event that occurs during the placing animation.
    public void BuildMycelium()
    {
        animator.SetBool(Building, false);
        // https://www.reddit.com/r/Unity2D/comments/d3mx3e/how_to_get_clicked_tile_in_a_tilemap/

        var tile = tilemap.GetTile<Tile>(_clickedBlock);

        // Only can place mycelium on mineshaft tiles (for some reason are seen as null)
        // Need to also check in bounds here, in case the player quickly moves their mouse while holding after the initial click?
        if (tile == null && InBounds(selectedPoint))
        {
            Debug.Log("PLACED MYCELIUM");
            // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
            _audioSource.clip = placeSound;
            _audioSource.pitch = Random.Range(.5f, 1f);
            _audioSource.Play();
            tilemap.SetTile(_clickedBlock, mineshaftWithMyceliumTile);
            resourceManager.myceliumPlaced(_clickedBlock);
        }
    }
}
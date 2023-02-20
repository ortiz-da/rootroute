using System.Linq;
using Unity.VisualScripting;
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
    private Vector3Int _clickedCell;

    // the origin block
    private int _originX;
    private int _originY;

    private static readonly int Mining = Animator.StringToHash("Mining");
    private static readonly int Building = Animator.StringToHash("Building");

    private Vector3Int mouseCell = Vector3Int.zero;


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
        IndicateMineableBlocks();
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            // https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/
            // https://stackoverflow.com/a/56519572


            // don't set the clicked block location if the player is in the middle of placing/mining.
            if (!(animator.GetBool(Mining) || animator.GetBool(Building)))
            {
                _clickedCell = backgroundTilemap.WorldToCell(camera1.ScreenToWorldPoint(Input.mousePosition));
            }

            /*
            Debug.Log("CLICKED ON: " + _clickedBlock);
            */


            // Left click to mine blocks
            // Only mine blocks if:
            // - the player is close enough and below ground
            // - the block isn't the core block, or bedrock
            if (Input.GetButton("Fire1"))
            {
                if (tilemap.GetTile(_clickedCell) != null)
                {
                    if (FindTilesAdjacentPlayer().Contains(_clickedCell) &&
                        InBounds(_clickedCell) &&
                        // todo allow for mining mycelium
                        /*backgroundTilemap.GetTile(_clickedCell) != mineshaftTile &&*/
                        // todo if get tile of a location that does not have a tile, throws null pointer?
                        !tilemap.GetTile(_clickedCell).name.Equals("WorldBorder1") &&
                        !(_clickedCell.x == _originX && _clickedCell.y == _originY))
                    {
                        animator.SetBool(Mining, true);
                    }
                }
            }

            // Right click to place mycelium.
            // Only can place mycelium on mineshaft tiles

            else if (Input.GetButton("Fire2"))
            {
                if (backgroundTilemap.GetTile(_clickedCell) != null)
                {
                    if (
                        FindTilesAdjacentPlayer().Contains(_clickedCell) &&
                        InBounds(_clickedCell) &&
                        backgroundTilemap.GetTile(_clickedCell) == mineshaftTile)
                    {
                        animator.SetBool(Building, true);
                    }
                }
            }
        }
    }

    // checks if a point is within tilemap
    private static bool InBounds(Vector3Int cell)
    {
        bool result = cell.y < VariableSetup.worldYSize && cell.y >= 0 && cell.x >= 0 &&
                      cell.x < VariableSetup.worldXSize;
        // Debug.Log(result);
        return result;
    }

    // Deletes a block that a player mined. Is called by an animation event that occurs on the last frame of the mine animation.
    public void DeleteBlock()
    {
        animator.SetBool(Mining, false);


        // Debug.Log(_clickedBlock);

        // Don't allow players to mine air, worldborder tiles, or origin block

        var clickedBlockName = tilemap.GetTile(_clickedCell).name;
        backgroundTilemap.SetTile(_clickedCell, mineshaftTile);
        tilemap.SetTile(_clickedCell, null);

        // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
        _audioSource.clip = digSound;
        _audioSource.pitch = Random.Range(.5f, 1f);
        _audioSource.Play();

        // Code for when player mines a mycelium tile.
        if (clickedBlockName.Equals("MyceliumRuleTile"))
            resourceManager.myceliumDeleted(_clickedCell);
    }


    // Places a mycelium block. Is called by an animation event that occurs during the placing animation.
    public void BuildMycelium()
    {
        animator.SetBool(Building, false);
        // https://www.reddit.com/r/Unity2D/comments/d3mx3e/how_to_get_clicked_tile_in_a_tilemap/

        // Debug.Log("PLACED MYCELIUM");
        // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
        _audioSource.clip = placeSound;
        _audioSource.pitch = Random.Range(.5f, 1f);
        _audioSource.Play();
        tilemap.SetTile(_clickedCell, mineshaftWithMyceliumTile);
        resourceManager.myceliumPlaced(_clickedCell);
    }

    private Vector3Int[] FindTilesAdjacentPlayer()
    {
        Vector3Int playerCell = tilemap.WorldToCell(this.transform.position);

        Vector3Int left = playerCell + Vector3Int.left;
        Vector3Int right = playerCell + Vector3Int.right;
        Vector3Int up = playerCell + Vector3Int.up;
        Vector3Int down = playerCell + Vector3Int.down;

        Vector3Int[] adjacentTiles = new[] { left, right, up, down, playerCell };

        /*Debug.Log("PLAYER AT: " + playerCell);
        Debug.Log("CAN MINE: " + left + " " + right + " " + up + " " + down);*/
        return adjacentTiles;
    }

    private void IndicateMineableBlocks()
    {
        // find tile that mouse is over
        // if a valid tile to mine, highlight it.

        Vector3 mousePos = camera1.ScreenToWorldPoint(Input.mousePosition);

        // if mouse was moved to a different cell since last frame
        if (mouseCell != tilemap.WorldToCell(mousePos))
        {
            // if the old cell had a tile in it
            if (tilemap.GetTile(mouseCell) != null && !(mouseCell.x == _originX && mouseCell.y == _originY))
            {
                // reset that tile back to its original color
                tilemap.SetColor(mouseCell, Color.white);
                tilemap.SetTileFlags(mouseCell, TileFlags.LockAll);
            }

            // update what cell the mouse is in
            mouseCell = tilemap.WorldToCell(mousePos);
        }

        if (FindTilesAdjacentPlayer().Contains(mouseCell))
        {
            if (tilemap.GetTile(mouseCell) != null && !tilemap.GetTile(mouseCell).name.Equals("WorldBorder1"))
            {
                tilemap.SetTileFlags(mouseCell, TileFlags.None);
                tilemap.SetColor(mouseCell, Color.green);
            }
        }
    }
}
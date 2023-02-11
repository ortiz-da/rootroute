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

    private readonly float mineDistance = 1.2f;

    private AudioSource audioSource;

    private Vector3Int clickedBlock;


    // Start is called before the first frame update
    private void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            // https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/
            // https://stackoverflow.com/a/56519572

            var selectedPoint = camera1.ScreenToWorldPoint(Input.mousePosition);
            selectedPoint = new Vector3(selectedPoint.x, selectedPoint.y, 0);
            var distance = Vector3.Distance(selectedPoint, transform.position);

            // Only mine blocks if the player is close enough and below ground
            if (distance <= mineDistance && inBounds(selectedPoint))
            {
                clickedBlock = tilemap.WorldToCell(selectedPoint);

                // Something fishy with names...
                // Debug.Log(tilemap.GetTile<Tile>(clickedBlock).name);
                // Left click to mine blocks
                if (Input.GetButton("Fire1"))
                    animator.SetBool("Mining", true);

                // Right click to place mycelium.
                else if (Input.GetButton("Fire2")) animator.SetBool("Building", true);
            }
        }
    }

    // checks if a point is within tilemap
    private bool inBounds(Vector3 selectedPoint)
    {
        return selectedPoint.y < VariableSetup.worldYSize && selectedPoint.y >= 0 && selectedPoint.x >= 0 &&
               selectedPoint.x < VariableSetup.worldXSize;
    }

    // Deletes a block that a player mined. Is called by an animation event that occurs on the last frame of the mine animation.
    public void DeleteBlock()
    {
        animator.SetBool("Mining", false);

        // Don't allow players to mine mineshaft tiles, or worldborder tiles
        // For some reason, the mineshaft tiles count as null?
        if (tilemap.GetTile(clickedBlock) != null &&
            !tilemap.GetTile(clickedBlock).name.Equals("WorldBorder1"))
        {
            backgroundTilemap.SetTile(clickedBlock, mineshaftTile);
            tilemap.SetTile(clickedBlock, null);

            // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
            audioSource.clip = digSound;
            audioSource.pitch = Random.Range(.5f, 1f);
            audioSource.Play();

            // Code for when player mines a mycelium tile.
            // idk why it thinks mycelium tiles are called mineshaftRuleTile
            if (tilemap.GetTile(clickedBlock).name.Equals("MineShaftRuleTile"))
                resourceManager.myceliumDeleted(clickedBlock);
            // Debug.Log("Successfully deleted mycelium");
        }
    }


    // Places a mycelium block. Is called by an animation event that occurs during the placing animation.
    public void BuildMycelium()
    {
        animator.SetBool("Building", false);
        // https://www.reddit.com/r/Unity2D/comments/d3mx3e/how_to_get_clicked_tile_in_a_tilemap/

        var tile = tilemap.GetTile<Tile>(clickedBlock);

        // Only can place mycelium on mineshaft tiles (for some reason are seen as null)
        if (tile == null)
        {
            // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
            audioSource.clip = placeSound;
            audioSource.pitch = Random.Range(.5f, 1f);
            audioSource.Play();
            tilemap.SetTile(clickedBlock, mineshaftWithMyceliumTile);
            resourceManager.myceliumPlaced(clickedBlock);
        }
    }
}
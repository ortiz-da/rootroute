using System;

using UnityEngine;

using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


public class Digging : MonoBehaviour
{

    public Tilemap tilemap;

    public Camera camera1;

    public Animator animator;

    private Vector3Int clickedBlock;
    
    public TileBase mineshaftTile;

    public TileBase mineshaftWithMyceliumTile;

    public ResourceManager resourceManager;

    private float mineDistance = 1.2f;
    
    private AudioSource audioSource;

    public AudioClip digSound;
    public AudioClip placeSound;


    // Start is called before the first frame update
    void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/
        // https://stackoverflow.com/a/56519572
        Vector3 selectedPoint = camera1.ScreenToWorldPoint(Input.mousePosition);
        selectedPoint = new Vector3(selectedPoint.x, selectedPoint.y, 0);

        float distance = Vector3.Distance(selectedPoint, transform.position);

        
        if (Input.GetButton("Fire1") && distance <= mineDistance )
        {
            // clicked on

            // Debug.Log("MINE BLOCK");
            clickedBlock = tilemap.WorldToCell(selectedPoint);

            animator.SetBool("Mining", true);

        }

        if (Input.GetButton("Fire2") && distance <= mineDistance)
        {
            // Debug.Log("PLACE BLOCK");
            clickedBlock = tilemap.WorldToCell(selectedPoint);
            animator.SetBool("Building", true);

        }

    }


    public void DeleteBlock()
    {
        animator.SetBool("Mining", false);

        if (tilemap.GetTile(clickedBlock) != null)
        {
            if (!(tilemap.GetTile(clickedBlock).name.Equals("grass")) && !(tilemap.GetTile(clickedBlock).name.Equals("WorldBorder1")))
            {
                tilemap.SetTile(clickedBlock, mineshaftTile);
                // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
                audioSource.clip = digSound;
                audioSource.pitch = Random.Range(.5f, 1f);
                audioSource.Play();
                if(tilemap.GetTile(clickedBlock).name.Equals("MyceliumRuleTile"))
                {
                    resourceManager.myceliumDeleted(tilemap.WorldToCell(clickedBlock));
                    Debug.Log("Successfully deleted mycelium");
                }
            }
        }

    }
    
    
    public void BuildMycelium()
    {
        animator.SetBool("Building", false);

// https://www.reddit.com/r/Unity2D/comments/d3mx3e/how_to_get_clicked_tile_in_a_tilemap/
            
        // Vector3Int tilemapPos = tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            
        Tile tile = tilemap.GetTile<Tile>(clickedBlock);

        //Debug.Log(tile);
        if (tile == null && !(tilemap.GetTile(clickedBlock).name.Equals("grass")))
        {
            // https://docs.unity3d.com/ScriptReference/AudioSource.Play.html
            audioSource.clip = placeSound;
            audioSource.pitch = Random.Range(.5f, 1f);
            audioSource.Play();
            tilemap.SetTile(clickedBlock, mineshaftWithMyceliumTile);
            resourceManager.myceliumPlaced(clickedBlock);
            //Debug.Log(tilemap.GetTile(clickedBlock).name);
        }

    }
    
}

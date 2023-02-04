using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class Digging : MonoBehaviour
{

    public Tilemap tilemap;

    public Camera camera1;

    public Animator animator;

    private Vector3Int clickedBlock;

    public TileBase myceliumTile;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/
        // https://stackoverflow.com/a/56519572
        Vector3 selectedPoint = camera1.ScreenToWorldPoint(Input.mousePosition);
        selectedPoint = new Vector3(selectedPoint.x, selectedPoint.y, 0);

        float distance = Vector3.Distance(selectedPoint, transform.position);

        
        if (Input.GetButton("Fire1") && distance <= 1f )
        {
            // clicked on

            Debug.Log("MINE BLOCK");
            clickedBlock = tilemap.WorldToCell(selectedPoint);
            
            animator.SetBool("Mining", true);

        }

        if (Input.GetButton("Fire2") && distance <= 1f)
        {
            Debug.Log("PLACE BLOCK");
            clickedBlock = tilemap.WorldToCell(selectedPoint);
            tilemap.SetTile(clickedBlock, myceliumTile);
            
        }

    }


    public void DeleteBlock()
    {
        animator.SetBool("Mining", false);

        if (tilemap.GetTile(clickedBlock) != null)
        {
            if (!(tilemap.GetTile(clickedBlock).name.Equals("grass")))
            {
                tilemap.SetTile(clickedBlock, null);
            }
        }

    }
    
}

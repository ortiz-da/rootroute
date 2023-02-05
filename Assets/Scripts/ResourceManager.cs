using NesScripts.Controls.PathFind;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public float biomass;

    NesScripts.Controls.PathFind.Point origin = new Point(-1, -1);

    float biomassRate = 0f;
   
    GameObject[] resources;
    List<GameObject> towers;
    bool[,] myceliumMap = new bool[31, 25];
    NesScripts.Controls.PathFind.Grid grid;
    void Start()
    {
        biomass = VariableSetup.biomass;
        resources = GameObject.FindGameObjectsWithTag("biomatter");
        foreach(GameObject resource in resources)
        {
            bioResource bio = resource.GetComponent<bioResource>();
            Point pos = makePoint(bio.position);
            myceliumMap[pos.x, pos.y] = true;
        }
        towers = new List<GameObject>();
        grid = new NesScripts.Controls.PathFind.Grid(myceliumMap);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void myceliumPlaced(Vector3Int spot)
    {
        Vector2Int corrected = correctPosition(spot);
        myceliumMap[corrected.x, corrected.y] = true;
        grid.UpdateGrid(myceliumMap);
        trace();
    }

    public void myceliumDeleted(Vector3Int spot)
    {
        Vector2Int corrected = correctPosition(spot);
        myceliumMap[corrected.x, corrected.y] = false;
        grid.UpdateGrid(myceliumMap);
        trace();
    }

    public void towerPlaced(GameObject tower)
    {
        
        //Vector2Int corrected = correctPosition(spot);
        //add tower to towers array
        trace();
    }

    private Vector2Int correctPosition(Vector3Int spot)
    {
        return new Vector2Int(spot.x + 16, spot.y - 3);
    }

    private Point makePoint(Vector3Int spot)
    {
        return new Point(spot.x + 16, spot.y - 3);
    }

    private void trace()
    {
        foreach(GameObject resource in resources)
        {
            bioResource bio = resource.GetComponent<bioResource>();
            Point pos = makePoint(bio.position);
            if(Pathfinding.FindPath(grid, pos, origin).Count == 0)
            {
                if(bio.connected)
                {
                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            else
            {
                bio.connected = true;
                biomassRate += bio.resourceRate;
            }
        }

        foreach(GameObject tower in towers)
        {
            //find path from origin to position of tower
            //if there is a valid path, it's powered
            //if there is a valid path subtract rate from biomatter increase rate 
        }
    }

    public void biomassUpdate(float cost)
    {
        biomassRate += cost;
    }

    
}

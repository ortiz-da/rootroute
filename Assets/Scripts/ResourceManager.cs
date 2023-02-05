using NesScripts.Controls.PathFind;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceManager : MonoBehaviour
{
    public float biomass;

    NesScripts.Controls.PathFind.Point origin = new Point(15, 0);

    float biomassRate = 0f;
   
    GameObject[] resources;
    List<GameObject> towers;
    bool[,] myceliumMap = new bool[31, 25];
    NesScripts.Controls.PathFind.Grid grid;
    void Start()
    {
        biomass = VariableSetup.biomass;
        resources = GameObject.FindGameObjectsWithTag("biomatter");
        
        myceliumMap[origin.x, origin.y] = true;
        towers = new List<GameObject>();
        grid = new NesScripts.Controls.PathFind.Grid(myceliumMap);
        StartCoroutine(biomassUpdate());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void myceliumPlaced(Vector3Int spot)
    {
        if (resources.Length == 0)
        {
            resources = GameObject.FindGameObjectsWithTag("biomatter");
            CheckResources();
        }
        Vector2Int corrected = correctPosition(spot);
        Debug.Log("Mycelium Placed! spot: " + corrected.ToString()); //(-1,1,0)
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

    private void CheckResources()
    {
        foreach (GameObject resource in resources)
        {
            bioResource bio = resource.GetComponent<bioResource>();
            Vector2Int corrected = correctPosition(bio.position);
            bio.correctedPosition = corrected;
            //Debug.Log(corrected.ToString());
            myceliumMap[corrected.x, corrected.y] = true;
        }
    }

    public void towerPlaced(GameObject tower)
    {
        Vector2Int corrected = correctPosition(tower.GetComponent<TowerAttack>().position);
        tower.GetComponent<TowerAttack>().correctedPosition = corrected;
        myceliumMap[corrected.x, corrected.y] = true;
        grid.UpdateGrid(myceliumMap);
        trace();
    }

    private Vector2Int correctPosition(Vector3Int spot)
    {
        return new Vector2Int(spot.x + 16, (spot.y - 3) * -1);
    }

    private Point makePoint(Vector3Int spot)
    {
        return new Point(spot.x + 16, (spot.y - 3) * -1);
    }

    private void trace()
    {
        if(Pathfinding.FindPath(grid, new Point(13, 5), origin).Count != 0)
        {
            //Debug.Log("Made a path!");
        }
        foreach(GameObject resource in resources)
        {
            bioResource bio = resource.GetComponent<bioResource>();
            Point pos = makePoint(bio.position);
            if(Pathfinding.FindPath(grid, pos, origin).Count == 0) //there is no path
            {
                if(bio.connected)
                {
                    bio.connected = false;
                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            else
            {
                Debug.Log("Path found to " + resource.name);
                bio.connected = true;
                biomassRate += bio.resourceRate;
            }
        }

        foreach(GameObject tower in towers)
        {
            TowerAttack towerAttack= tower.GetComponent<TowerAttack>();
            Point pos = makePoint(towerAttack.position);
            if (Pathfinding.FindPath(grid, pos, origin).Count == 0) //there is no path
            {
                if (towerAttack.connected)
                {
                    towerAttack.connected = false;
                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            else
            {
                towerAttack.connected = true;
                biomass -= VariableSetup.tower1Cost;
            }
        }
    }

    public void biomassUpdate(float cost)
    {
        biomassRate += cost;
    }

    IEnumerator biomassUpdate()
    {
        biomass += biomassRate;
        yield return new WaitForSeconds(VariableSetup.rate);
    }
}

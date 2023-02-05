using NesScripts.Controls.PathFind;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceManager : MonoBehaviour
{
    public float biomass;

    NesScripts.Controls.PathFind.Point origin = new Point(15, 0);

    public float biomassRate;

    GameObject[] resources;
    List<GameObject> towers;
    bool[,] myceliumMap = new bool[31, 25];
    NesScripts.Controls.PathFind.Grid grid;
    void Start()
    {
        biomass = VariableSetup.startingBiomass;
        biomassRate = 0f;
        resources = GameObject.FindGameObjectsWithTag("biomatter");

        myceliumMap[origin.x, origin.y] = true;
        towers = new List<GameObject>();
        grid = new NesScripts.Controls.PathFind.Grid(myceliumMap);
        StartCoroutine(biomassCounterUpdate());
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
        biomass -= VariableSetup.tower1Cost;

        Debug.Log("In resourcemanager " + tower.GetComponent<TowerAttack2>().position.ToString());
        Vector2Int corrected = correctPosition(tower.GetComponent<TowerAttack2>().position);
        tower.GetComponent<TowerAttack2>().correctedPosition = corrected;
        Debug.Log("Tower at: " + corrected.ToString());

        towers.Add(tower);

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
        foreach (GameObject resource in resources)
        {
            bioResource bio = resource.GetComponent<bioResource>();
            Point pos = makePoint(bio.position);
            if (Pathfinding.FindPath(grid, pos, origin).Count == 0) //there is no path
            {
                if (bio.connected)
                {
                    bio.connected = false;
                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            // Only connect once
            else if (!bio.connected)
            {
                Debug.Log("Path found to " + resource.name);
                bio.connected = true;
                biomassRate += bio.resourceRate;
            }
        }

        foreach (GameObject tower in towers)
        {
            TowerAttack2 towerAttack2 = tower.GetComponent<TowerAttack2>();
            Point pos = makePoint(towerAttack2.position);
            if (Pathfinding.FindPath(grid, pos, origin).Count == 0) //there is no path
            {
                if (towerAttack2.connected)
                {
                    towerAttack2.connected = false;
                    Debug.Log("disconnected tower!");
                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            // Only connect once
            else if (!towerAttack2.connected)
            {
                Debug.Log("Tower is connected");
                towerAttack2.connected = true;
            }
        }
    }

    public void biomassUpdate(float cost)
    {
        biomass += cost;
    }

    IEnumerator biomassCounterUpdate()
    {
        biomass += biomassRate;
        yield return new WaitForSeconds(VariableSetup.rate);
    }
}
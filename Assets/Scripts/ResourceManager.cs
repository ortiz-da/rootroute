using NesScripts.Controls.PathFind;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResourceManager : MonoBehaviour
{
    public float biomass;

    private NesScripts.Controls.PathFind.Point origin;

    public float biomassRate;

    GameObject[] resources;
    List<GameObject> towers;
    private bool[,] myceliumMap;
    NesScripts.Controls.PathFind.Grid grid;

    public GameObject falseMarker;
    public GameObject trueMarker;

    public Tilemap _tilemap;
    void Start()
    {
        biomass = VariableSetup.startingBiomass;
        biomassRate = 0f;
        resources = GameObject.FindGameObjectsWithTag("biomatter");
        _tilemap = GameObject.Find("Grid").transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        
        // 00 is now bottom left.
        // so origin point is at the top

        int originX = (_tilemap.size.x / 2) ;
        int originY = _tilemap.size.y - 1;
        origin = new Point(originX, originY);
        Debug.Log(originX);
        Debug.Log(originY);

        // size of 2d array now depends on size of tilemap
        myceliumMap  = new bool[_tilemap.size.x, _tilemap.size.y];
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
        // Vector2Int corrected = correctPosition(spot);
        Debug.Log("Mycelium Placed! spot: " + spot); //(-1,1,0)
        myceliumMap[spot.x, spot.y] = true;
        grid.UpdateGrid(myceliumMap);
        trace();
    }

    public void myceliumDeleted(Vector3Int spot)
    {
        Debug.Log("Mycelium deleted! spot: " + spot); //(-1,1,0)

        myceliumMap[spot.x, spot.y] = false;
        grid.UpdateGrid(myceliumMap);
        trace();
    }

    private void CheckResources()
    {
        foreach (GameObject resource in resources)
        {
            bioResource bio = resource.GetComponent<bioResource>();

            // Debug.Log(corrected.ToString());
            myceliumMap[bio.position.x, bio.position.y] = true;
        }
    }

    public void towerPlaced(GameObject tower)
    {
        biomass -= VariableSetup.tower1Cost;

        var towerPos = tower.GetComponent<TowerAttack2>().position;
        Debug.Log("Tower at: " + towerPos.ToString());

        towers.Add(tower);

        myceliumMap[towerPos.x, towerPos.y] = true;
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
            List<Point> path = Pathfinding.FindPath(grid, pos, origin, Pathfinding.DistanceType.Manhattan);
            if (path.Count == 0) //there is no path
            {
                if (bio.connected)
                {
                    bio.connected = false;
                    bio.gameObject.GetComponent<Animator>().SetBool("PossumConnected", false);

                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            // Only connect once
            else if(!bio.connected)
            {
                Debug.Log("Path found to " + resource.name);
                bio.connected = true;
                bio.gameObject.GetComponent<Animator>().SetBool("PossumConnected", true);
                biomassRate += bio.resourceRate;
            }
        }

        foreach (GameObject tower in towers)
        {
            TowerAttack2 towerAttack2 = tower.GetComponent<TowerAttack2>();
            Point pos = makePoint(towerAttack2.position);
            if (Pathfinding.FindPath(grid, pos, origin, Pathfinding.DistanceType.Manhattan).Count == 0) //there is no path
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
        while (!LevelManager.isGameOver)
        {
            //Debug.Log("adding " + biomassRate);
            biomass += biomassRate;
            yield return new WaitForSeconds(VariableSetup.rate);
        }
    }
}
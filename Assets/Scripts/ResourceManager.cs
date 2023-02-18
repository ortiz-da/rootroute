using System.Collections;
using System.Collections.Generic;
using System.Text;
using NesScripts.Controls.PathFind;
using UnityEngine;
using UnityEngine.Tilemaps;
using Grid = NesScripts.Controls.PathFind.Grid;

public class ResourceManager : MonoBehaviour
{
    public float biomass;

    public float biomassRate;

    public GameObject falseMarker;
    public GameObject trueMarker;

    public Tilemap _tilemap;
    private Grid grid;
    private bool[,] myceliumMap;

    private Point origin;

    private GameObject[] resources;
    public List<GameObject> towers;

    private void Start()
    {
        biomass = VariableSetup.startingBiomass;
        biomassRate = 0f;

        // todo possibly will need to fix once biomass can appear/dissappear
        resources = GameObject.FindGameObjectsWithTag("biomatter");

        // 00 is now bottom left.
        // so origin point is at the top

        var originX = _tilemap.size.x / 2;
        var originY = _tilemap.size.y - 1;
        origin = new Point(originX, originY);


        // size of 2d array now depends on size of tilemap
        // add 1 to y to account for grass layer that can be "connected" since it has towers on it
        myceliumMap = new bool[_tilemap.size.x, _tilemap.size.y + 1];
        myceliumMap[origin.x, origin.y] = true;
        towers = new List<GameObject>();
        grid = new Grid(myceliumMap);


        // printMyceliumGrid();

        StartCoroutine(biomassCounterUpdate());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    // prints "upside down"
    private void printMyceliumGrid()
    {
        // https://www.reddit.com/r/Unity3D/comments/dc3ttd/how_to_print_a_2d_array_to_the_unity_console_as_a/
        var sb = new StringBuilder();
        for (var y = 0; y < myceliumMap.GetLength(1); y++)
        {
            for (var x = 0; x < myceliumMap.GetLength(0); x++) sb.Append(myceliumMap[x, y] ? "1" : "0");

            sb.AppendLine();
        }

        Debug.Log(sb.ToString());
    }

    public void myceliumPlaced(Vector3Int spot)
    {
        // Will not find resources if called right at start, cuz that's when they are spawning?
        if (resources.Length == 0) resources = GameObject.FindGameObjectsWithTag("biomatter");
        // CheckResources();

        myceliumMap[spot.x, spot.y] = true;
        /*
        printMyceliumGrid();
        */
        grid.UpdateGrid(myceliumMap);
        trace();
    }

    public void myceliumDeleted(Vector3Int spot)
    {
        Debug.Log("MYCELIUM DELETED");
        myceliumMap[spot.x, spot.y] = false;
        grid.UpdateGrid(myceliumMap);
        trace();
    }

    /*
    private void CheckResources()
    {
        foreach (var resource in resources)
        {
            var bio = resource.GetComponent<bioResource>();

            // Debug.Log(corrected.ToString());
            myceliumMap[bio.position.x, bio.position.y] = true;
        }
    }
    */

    // tower added, and its mycelium pos
    public void towerPlaced(GameObject tower)
    {
        biomass -= VariableSetup.tower1Cost;

        var myceliumConnectorPosition = tower.GetComponent<TowerAttack2>().myceliumConnectorPosition;

        towers.Add(tower);
        myceliumMap[myceliumConnectorPosition.x, myceliumConnectorPosition.y] = true;
        grid.UpdateGrid(myceliumMap);
        // printMyceliumGrid();

        trace();
    }


    private Point makePoint(Vector3Int spot)
    {
        return new Point(spot.x, spot.y);
    }

    private void trace()
    {
        // printMyceliumGrid();

        // Check which resources are connected (if any)
        foreach (var resource in resources)
        {
            var bio = resource.GetComponent<bioResource>();
            var pos = makePoint(bio.position);
            var path = Pathfinding.FindPath(grid, pos, origin, Pathfinding.DistanceType.Manhattan);

            if (path.Count == 0) //there is no path
            {
                if (bio.connected)
                {
                    bio.disconnectResource();
                    biomassRate -= bio.resourceRate;


                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            // Only connect once
            else if (!bio.connected)
            {
                Debug.Log("Path found to " + resource.name);
                bio.connectResource();
                biomassRate += bio.resourceRate;
            }
        }

        // Check which towers are connected (if any)
        foreach (var tower in towers)
        {
            // Debug.Log(tower.transform.position);
            // Get script attached to tower
            var towerAttack2 = tower.GetComponent<TowerAttack2>();
            // position 2 blocks below tower's location
            var pos = makePoint(towerAttack2.myceliumConnectorPosition);


            // Debug.Log("PATHFINDING FROM: (" + pos.x + ", " + pos.y + ")" + " TO: (" + origin.x + ", " + origin.y + ")");

            if (Pathfinding.FindPath(grid, pos, origin, Pathfinding.DistanceType.Manhattan).Count ==
                0) //there is no path
            {
                if (towerAttack2.connected)
                {
                    towerAttack2.disconnectTower();
                    Debug.Log("disconnected tower!");
                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            // Only connect once
            else if (!towerAttack2.connected)
            {
                towerAttack2.connectTower();
            }
        }
    }

    // change the total biomass number by the given cost
    public void biomassUpdate(float cost)
    {
        biomass += cost;
    }

    // Coroutine that runs every second. Increases the total biomass by the current rate
    private IEnumerator biomassCounterUpdate()
    {
        while (!LevelManager.isGameOver)
        {
            //Debug.Log("adding " + biomassRate);
            biomass += biomassRate;
            yield return new WaitForSeconds(VariableSetup.rate);
        }
    }

    // Updates list of resources
    public void ReFindBiomass()
    {
        resources = GameObject.FindGameObjectsWithTag("biomatter");
    }
}
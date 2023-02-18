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

    public Tilemap tilemap;
    private Grid _grid;
    private bool[,] _myceliumMap;

    private Point _origin;

    private GameObject[] _resources;
    public List<GameObject> towers;

    private void Start()
    {
        biomass = VariableSetup.startingBiomass;
        biomassRate = 0f;

        // todo possibly will need to fix once biomass can appear/dissappear
        _resources = GameObject.FindGameObjectsWithTag("biomatter");

        // 00 is now bottom left.
        // so origin point is at the top

        var tilemapSize = tilemap.size;
        var originX = tilemapSize.x / 2;
        var originY = tilemapSize.y - 1;
        _origin = new Point(originX, originY);


        // size of 2d array now depends on size of tilemap
        // add 1 to y to account for grass layer that can be "connected" since it has towers on it
        _myceliumMap = new bool[tilemapSize.x, tilemapSize.y + 1];
        _myceliumMap[_origin.x, _origin.y] = true;
        towers = new List<GameObject>();
        _grid = new Grid(_myceliumMap);


        // printMyceliumGrid();

        StartCoroutine(BiomassCounterUpdate());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    // prints "upside down"
    private void PrintMyceliumGrid()
    {
        // https://www.reddit.com/r/Unity3D/comments/dc3ttd/how_to_print_a_2d_array_to_the_unity_console_as_a/
        var sb = new StringBuilder();
        for (var y = 0; y < _myceliumMap.GetLength(1); y++)
        {
            for (var x = 0; x < _myceliumMap.GetLength(0); x++) sb.Append(_myceliumMap[x, y] ? "1" : "0");

            sb.AppendLine();
        }

        Debug.Log(sb.ToString());
    }

    public void MyceliumPlaced(Vector3Int spot)
    {
        // Will not find resources if called right at start, cuz that's when they are spawning?
        if (_resources.Length == 0) _resources = GameObject.FindGameObjectsWithTag("biomatter");
        // CheckResources();

        _myceliumMap[spot.x, spot.y] = true;
        /*
        printMyceliumGrid();
        */
        _grid.UpdateGrid(_myceliumMap);
        Trace();
    }

    public void MyceliumDeleted(Vector3Int spot)
    {
        Debug.Log("MYCELIUM DELETED");
        _myceliumMap[spot.x, spot.y] = false;
        _grid.UpdateGrid(_myceliumMap);
        Trace();
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
    public void TowerPlaced(GameObject tower)
    {
        biomass -= VariableSetup.tower1Cost;

        var myceliumConnectorPosition = tower.GetComponent<TowerAttack2>().myceliumConnectorPosition;

        towers.Add(tower);
        _myceliumMap[myceliumConnectorPosition.x, myceliumConnectorPosition.y] = true;
        _grid.UpdateGrid(_myceliumMap);
        // printMyceliumGrid();

        Trace();
    }


    private Point MakePoint(Vector3Int spot)
    {
        return new Point(spot.x, spot.y);
    }

    private void Trace()
    {
        // printMyceliumGrid();

        // Check which resources are connected (if any)
        foreach (var resource in _resources)
        {
            var bio = resource.GetComponent<BioResource>();
            var pos = MakePoint(bio.position);
            var path = Pathfinding.FindPath(_grid, pos, _origin, Pathfinding.DistanceType.Manhattan);

            if (path.Count == 0) //there is no path
            {
                if (bio.connected)
                {
                    bio.DisconnectResource();
                    biomassRate -= bio.resourceRate;


                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            // Only connect once
            else if (!bio.connected)
            {
                Debug.Log("Path found to " + resource.name);
                bio.ConnectResource();
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
            var pos = MakePoint(towerAttack2.myceliumConnectorPosition);


            // Debug.Log("PATHFINDING FROM: (" + pos.x + ", " + pos.y + ")" + " TO: (" + origin.x + ", " + origin.y + ")");

            if (Pathfinding.FindPath(_grid, pos, _origin, Pathfinding.DistanceType.Manhattan).Count ==
                0) //there is no path
            {
                if (towerAttack2.connected)
                {
                    towerAttack2.DisconnectTower();
                    Debug.Log("disconnected tower!");
                    //if it was already connected and now isn't, something has broken the chain
                    //we will need to call the users attention to the break
                }
            }
            // Only connect once
            else if (!towerAttack2.connected)
            {
                towerAttack2.ConnectTower();
            }
        }
    }

    // change the total biomass number by the given cost
    public void BiomassUpdate(float cost)
    {
        biomass += cost;
    }

    // Coroutine that runs every second. Increases the total biomass by the current rate
    private IEnumerator BiomassCounterUpdate()
    {
        // used to check while game is not over, might be good to have that?
        while (true)
        {
            //Debug.Log("adding " + biomassRate);
            biomass += biomassRate;
            yield return new WaitForSeconds(VariableSetup.rate);
        }
    }

    // Updates list of resources
    public void ReFindBiomass()
    {
        _resources = GameObject.FindGameObjectsWithTag("biomatter");
    }
}
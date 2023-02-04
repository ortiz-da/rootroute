using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public float biomass;

    float biomassRate = 0f;

    GameObject[] resources;
    GameObject[] towers;
    bool[,] myceliumMap = new bool[31, 25];
    void Start()
    {
        biomass = VariableSetup.biomass;
        //read in all biomatter in the scene and add them to the resources array
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void myceliumPlaced(Vector3Int spot)
    {
        Vector2Int corrected = correctPosition(spot);
        myceliumMap[corrected.x, corrected.y] = true;
        trace();
    }

    public void towerPlaced(Vector3Int spot)
    {
        Vector2Int corrected = correctPosition(spot);
        trace();
    }

    private Vector2Int correctPosition(Vector3Int spot)
    {
        return new Vector2Int(spot.x + 16, spot.y - 3);
    }

    private void trace()
    {
        foreach(GameObject resource in resources)
        {
            //find path from origin to position of resource
            //if there's a valid path, set "Found" variable in resource to true
            //if there's a valid path, add providing rate to biomatter increase rate
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

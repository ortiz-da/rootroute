using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public float biomass;

    float biomassRate = 0f;
    bool[,] myceliumMap = new bool[31, 25];
    void Start()
    {
        biomass = VariableSetup.biomass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newResource()
    {

    }

    public void myceliumPlaced(Vector3Int spot)
    {
        myceliumMap[spot.x + 16, spot.y - 3] = true;
    }

    
}

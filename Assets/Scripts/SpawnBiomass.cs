using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBiomass : MonoBehaviour
{
    private int numBiomass = 10;

    public GameObject biomass;
    // Start is called before the first frame update
    void Start()
    {
        while (numBiomass >= 0)
        {
            // https://docs.unity3d.com/ScriptReference/Random.Range.html
            var position = new Vector3(Random.Range(-15f, 15f), Random.Range(-18f,2f), 0);
            Instantiate(biomass, position, Quaternion.identity);
            numBiomass--;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

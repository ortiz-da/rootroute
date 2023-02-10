using UnityEngine;

public class SpawnBiomass : MonoBehaviour
{
    public GameObject biomass;

    private int currentNumBiomass;

    // Start is called before the first frame update
    private void Start()
    {
        while (currentNumBiomass < VariableSetup.maxNumBiomass)
        {
            // https://docs.unity3d.com/ScriptReference/Random.Range.html
            var position = new Vector3(Random.Range(0f, VariableSetup.worldXSize),
                Random.Range(0f, VariableSetup.worldYSize), 0);
            Instantiate(biomass, position, Quaternion.identity);
            currentNumBiomass++;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
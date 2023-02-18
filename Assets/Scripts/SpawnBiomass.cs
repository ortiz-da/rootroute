using UnityEngine;

public class SpawnBiomass : MonoBehaviour
{
    public GameObject biomass;

    private int _currentNumBiomass;

    // Start is called before the first frame update
    private void Start()
    {
        while (_currentNumBiomass < VariableSetup.maxNumBiomass)
        {
            // https://docs.unity3d.com/ScriptReference/Random.Range.html
            var position = new Vector3(Random.Range(0, VariableSetup.worldXSize),
                Random.Range(0, VariableSetup.worldYSize), 0);
            Instantiate(biomass, position, Quaternion.identity);
            _currentNumBiomass++;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
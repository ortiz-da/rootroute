using System.Collections;
using UnityEngine;

public class WormSpawn : MonoBehaviour
{
    public GameObject worm;

    private int numWorms;

    private void Start()
    {
        StartCoroutine(spawnWorm());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator spawnWorm()
    {
        yield return new WaitForSeconds(10);
        var position = new Vector3(Random.Range(0f, VariableSetup.worldXSize),
            Random.Range(0f, VariableSetup.worldYSize), 0);
        while (!LevelManager.isGameOver && numWorms <= VariableSetup.maxWorms)
        {
            Instantiate(worm, position, Quaternion.identity);
            numWorms++;
            yield return new WaitForSeconds(VariableSetup.wormSpawnRate);
        }
    }
}
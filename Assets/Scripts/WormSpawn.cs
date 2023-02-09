using System.Collections;
using UnityEngine;

public class WormSpawn : MonoBehaviour
{
    public GameObject worm;

    private int numWorms = 0;

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
        yield return new WaitForSeconds(30);
        var position = new Vector3(Random.Range(-15f, 15f), Random.Range(-20f, 2f), 0);
        while (!LevelManager.isGameOver && numWorms <= VariableSetup.maxWorms)
        {
            Instantiate(worm, position, Quaternion.identity);
            numWorms++;
            yield return new WaitForSeconds(Random.Range(30, 60));
        }
    }
}
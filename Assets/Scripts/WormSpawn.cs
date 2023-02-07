using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSpawn : MonoBehaviour
{

    public GameObject worm;
    void Start()
    {
        StartCoroutine(spawnWorm());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator spawnWorm()
    {
        yield return new WaitForSeconds(30);
        Vector3 position = new Vector3(Random.Range(-15f, 15f), Random.Range(-20f, 2f), 0);
        while (!LevelManager.isGameOver)
        {
            Instantiate(worm, position, Quaternion.identity);
        }
    }
}

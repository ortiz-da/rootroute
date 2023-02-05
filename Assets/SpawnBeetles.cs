using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeetles : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject beetle;

    private int waveNumber = 0;
    void Start()
    {
        StartCoroutine(SpawnBeetle());

    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnBeetle()
    {
        while (waveNumber < 5)
        {

            SpawnWave();
            
            yield return new WaitForSeconds(5);
            waveNumber++;
        }

    }

    private void SpawnWave()
    {
        Instantiate(beetle);
        Instantiate(beetle);
        Instantiate(beetle);
    }
}

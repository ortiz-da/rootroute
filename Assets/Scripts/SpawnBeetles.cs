using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeetles : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject beetle;

    private int waveNumber = 0;

    private int maxSpawn = 3;

    private AudioSource audioSource;
    public AudioClip waveStartSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnBeetle());
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnBeetle()
    {
        yield return new WaitForSeconds(10);
        while (waveNumber < 15)
        {
            maxSpawn++;
            SpawnWave();
            
            yield return new WaitForSeconds(Random.Range(5, 15));
            waveNumber++;
        }

    }

    IEnumerator SpawnWave()
    {
        audioSource.clip = waveStartSound;
        audioSource.Play();
        for(int i = 0; i < Random.Range(3, maxSpawn); i++) 
        {
            Instantiate(beetle);
            yield return new WaitForSeconds(Random.Range(0f, 2f));
        }
    }
}

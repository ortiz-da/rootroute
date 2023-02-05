using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeetles : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject beetle;

    private int waveNumber = 0;

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

            SpawnWave();
            
            yield return new WaitForSeconds(10);
            waveNumber++;
        }

    }

    private void SpawnWave()
    {
        audioSource.clip = waveStartSound;
        audioSource.Play();

        Instantiate(beetle);
        Instantiate(beetle);
        Instantiate(beetle);
    }
}

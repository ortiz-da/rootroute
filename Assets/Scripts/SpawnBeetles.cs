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

    public GameObject spawnRight;

    //private Quaternion flipped;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnBeetle());
        //flipped = new Quaternion(Quaternion.identity.x, -Quaternion.identity.y, -Quaternion.identity.z, Quaternion.identity.w);
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
            StartCoroutine(SpawnWave());

            yield return new WaitForSeconds(Random.Range(5, 15));
            waveNumber++;
        }

    }

    IEnumerator SpawnWave()
    {
        audioSource.clip = waveStartSound;
        audioSource.Play();
        for (int i = 0; i < Random.Range(3, maxSpawn); i++)
        {
            if(waveNumber > 2 && Random.Range(1, 3) == 2) 
            {
                GameObject thisbeetle = Instantiate(beetle, spawnRight.transform.position, Quaternion.identity);
                Vector3 theScale = thisbeetle.transform.localScale;
                theScale.x *= -1;
                thisbeetle.transform.localScale = theScale;
            }
            else
                Instantiate(beetle);
            yield return new WaitForSeconds(Random.Range(0f, 2f));
        }
    }
}
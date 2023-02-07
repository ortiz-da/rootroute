using System.Collections;
using UnityEngine;

public class SpawnBeetles : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject beetle;
    public AudioClip waveStartSound;

    private AudioSource audioSource;

    private readonly int maxSpawn = 5;

    private int waveNumber;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnBeetle());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator SpawnBeetle()
    {
        yield return new WaitForSeconds(10);
        while (waveNumber < 15)
        {
            SpawnWave();

            yield return new WaitForSeconds(Random.Range(5, 15));
            waveNumber++;
        }
    }

    private IEnumerator SpawnWave()
    {
        audioSource.clip = waveStartSound;
        audioSource.Play();
        for (var i = 0; i < Random.Range(3, maxSpawn); i++)
        {
            Instantiate(beetle);
            yield return new WaitForSeconds(Random.Range(0f, 2f));
        }
    }
}
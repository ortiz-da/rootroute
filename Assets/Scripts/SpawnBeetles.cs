using System.Collections;
using UnityEngine;

public class SpawnBeetles : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject beetle;
    public AudioClip waveStartSound;

    private AudioSource audioSource;

    private int maxSpawn = 3;

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
            audioSource.clip = waveStartSound;
            audioSource.Play();


            for (var i = 0; i < Random.Range(3, maxSpawn); i++)
            {
                Instantiate(beetle);
                yield return new WaitForSeconds(Random.Range(0f, 2f));
            }

            yield return new WaitForSeconds(Random.Range(10, 15));
            waveNumber++;
            maxSpawn++;
        }
    }
}
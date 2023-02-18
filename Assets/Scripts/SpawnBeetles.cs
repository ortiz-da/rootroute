using System.Collections;
using UnityEngine;

public class SpawnBeetles : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject beetle;
    public AudioClip waveStartSound;

    private AudioSource _audioSource;

    private int _maxSpawn = 3;

    private int _waveNumber;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnBeetle());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator SpawnBeetle()
    {
        yield return new WaitForSeconds(10);
        while (_waveNumber < 15)
        {
            _audioSource.clip = waveStartSound;
            _audioSource.Play();


            for (var i = 0; i < Random.Range(3, _maxSpawn); i++)
            {
                Instantiate(beetle);
                yield return new WaitForSeconds(Random.Range(0f, 2f));
            }

            yield return new WaitForSeconds(Random.Range(10, 15));
            _waveNumber++;
            _maxSpawn++;
        }
    }
}
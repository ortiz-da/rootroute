using System.Collections;
using TMPro;
using UnityEngine;


public class RunWaves : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip waveStartSound;

    private int _waveNumber = 1;

    public GameObject[] spawners;

    public GameObject[] enemies;

    public TextMeshProUGUI waveText;

    public int firstWaveDelay = 30;


    // Start is called before the first frame update
    void Start()
    {
        waveText.text = "Wave: " + _waveNumber;

        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnWave());
    }

    public void SetFirstWaveDelay(int delay)
    {
        this.firstWaveDelay = delay;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator SpawnWave()
    {
        // to get ready before waves
        yield return new WaitForSeconds(firstWaveDelay);
        while (_waveNumber < VariableSetup.numWaves)
        {
            // Play sound indicating start of wave
            _audioSource.clip = waveStartSound;
            _audioSource.Play();

            // spawn some enemies (max possible increases each round),
            // randomly choosing which spawn point to start them at
            for (var i = 0; i < Random.Range(3, _waveNumber); i++)
            {
                // first 3 waves, only spawn at first spawner (left side of tree)
                int spawnerIndex = _waveNumber < 3 ? 0 : Random.Range(0, spawners.Length);
                Vector3 spawnPosition = spawners[spawnerIndex].transform.position;

                // spawn different types of enemies in later waves
                int enemyType = _waveNumber < 3 ? 0 : Random.Range(0, enemies.Length);

                Instantiate(
                    enemies[enemyType],
                    spawnPosition,
                    // Flip enemy if they are on the right of the tree so that they are facing it.
                    // http://answers.unity.com/answers/952729/view.html
                    spawnerIndex == 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0));
                // Delay between individual enemy spawns so they don't always spawn on top of each other.
                yield return new WaitForSeconds(Random.Range(0f, 2f));
            }

            // Wait between waves
            yield return new WaitForSeconds(Random.Range(10, 15 + _waveNumber));
            _waveNumber++;
            waveText.text = "Wave: " + _waveNumber;
        }
    }
}
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

// USING https://youtu.be/8oTYabhj248
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public string[] lines;
    private const float TextSpeed = .07f;
    private int _index;
    public GameObject highlightFrame;

    public GameObject nextButton;

    //public GameObject player;

    public bool locked;

    //public Tilemap tilemap;

    private GameObject _instantiatedFrame;

    private ResourceManager _resourceManager;

    public GameObject waveManager;

    public GameObject possum;

    private AudioSource _audioSource;

    private LevelManager _levelManager;


    // Start is called before the first frame update
    void Start()
    {
        _resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        waveManager.SetActive(false);

        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();


        //tilemap = GameObject.Find("Grid").transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        tmp.text = string.Empty;
        _audioSource = GetComponent<AudioSource>();
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUnlockCondition();


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (tmp.text == lines[_index] && !locked)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                tmp.text = lines[_index];
            }
        }

        if (_index < lines.Length)
        {
            nextButton.gameObject.SetActive(tmp.text == lines[_index]);
        }
    }

    void StartDialogue()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[_index])
        {
            tmp.text += c;

            if (!c.Equals(' '))
            {
                _audioSource.PlayOneShot(_audioSource.clip);
            }

            yield return new WaitForSeconds(TextSpeed);
        }
    }

    void NextLine()
    {
        if (_index < lines.Length - 1)
        {
            _index++;
            RunTutorialStep();
            tmp.text = String.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // tutorial completed
            _levelManager.LoadNextScene();
            gameObject.SetActive(false);
        }
    }

    private void RunTutorialStep()
    {
        if (_index == 2)
        {
            _instantiatedFrame = Instantiate(highlightFrame, new Vector3Int(9, 24, 0), Quaternion.identity);
            _instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer = false;
            locked = true;
        }

        else if (_index == 3)
        {
            locked = true;
        }

        else if (_index == 5)
        {
            _instantiatedFrame = Instantiate(highlightFrame, new Vector3Int(15, 24, 0), Quaternion.identity);
            _instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer = false;
            locked = true;
        }
        else if (_index == 6)
        {
            waveManager.GetComponent<RunWaves>().SetFirstWaveDelay(0);
            waveManager.SetActive(true);
            locked = true;
        }
        else if (_index == 7)
        {
            // todo stop wave manager

            // TODO draw attention to lower biomass
        }
        else if (_index == 8)
        {
            locked = true;
            Instantiate(possum, new Vector3(5, 24, 0), Quaternion.identity);
            _instantiatedFrame = Instantiate(highlightFrame, new Vector3Int(5, 24, 0), Quaternion.identity);
            _resourceManager.ReFindBiomass();
        }
        else
        {
//            instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer = true;
        }
    }

    private void CheckUnlockCondition()
    {
        if (_index == 2)
        {
            // if it was locked last frame, and now isn't, then the player did what they needed to do.
            // so play the next line automatically
            if (_instantiatedFrame != null)
            {
                if (locked == _instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer)
                {
                    locked = false;
                    Destroy(_instantiatedFrame);
                    StopAllCoroutines();
                    tmp.text = lines[_index];
                    NextLine();
                }
            }
        }

        else if (_index == 3)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                locked = false;
                StopAllCoroutines();
                tmp.text = lines[_index];
                NextLine();
            }
        }

        // todo bug if they complete this step in 4
        else if (_index == 5)
        {
            if (_resourceManager.towers.First().GetComponent<TowerAttack2>().connected)
            {
                Debug.Log("CONNECTED TOWER");
                if (_instantiatedFrame != null)
                {
                    locked = false;
                    Destroy(_instantiatedFrame);
                    StopAllCoroutines();
                    tmp.text = lines[_index];
                    NextLine();
                }
            }
        }

        // advance when all termites gone
        else if (_index == 6)
        {
            StartCoroutine(CheckEnemyCount());
        }

        else if (_index == 8)
        {
            if (_resourceManager.biomassRate > 0)
            {
                locked = false;
                Destroy(_instantiatedFrame);
                StopAllCoroutines();
                tmp.text = lines[_index];
                NextLine();
            }
        }
    }

    private IEnumerator CheckEnemyCount()
    {
        yield return new WaitForSeconds(1);
        if (GameObject.FindGameObjectsWithTag("AGEnemy").Length == 0)
        {
            locked = false;
            waveManager.SetActive(false);
            StopAllCoroutines();
            NextLine();
        }
    }
}
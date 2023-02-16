using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

// USING https://youtu.be/8oTYabhj248
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public string[] lines;
    private float textSpeed = .03f;
    private int index;
    public GameObject highlightFrame;

    public GameObject nextButton;

    //public GameObject player;

    public bool locked = false;

    //public Tilemap tilemap;

    private GameObject instantiatedFrame;

    private ResourceManager _resourceManager;

    public GameObject waveManager;

    public GameObject possum;

    // Start is called before the first frame update
    void Start()
    {
        _resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        waveManager.SetActive(false);

        //tilemap = GameObject.Find("Grid").transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        tmp.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUnlockCondition();


        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (tmp.text == lines[index] && !locked)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                tmp.text = lines[index];
            }
        }

        if (index < lines.Length)
        {
            nextButton.gameObject.SetActive(tmp.text == lines[index]);
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            tmp.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            RunTutorialStep();
            tmp.text = String.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void RunTutorialStep()
    {
        if (index == 2)
        {
            instantiatedFrame = Instantiate(highlightFrame, new Vector3Int(9, 24, 0), Quaternion.identity);
            instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer = false;
            locked = true;
        }

        else if (index == 3)
        {
            locked = true;
        }

        else if (index == 5)
        {
            instantiatedFrame = Instantiate(highlightFrame, new Vector3Int(15, 24, 0), Quaternion.identity);
            instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer = false;
            locked = true;
        }
        else if (index == 6)
        {
            waveManager.GetComponent<RunWaves>().setFirstWaveDelay(0);
            waveManager.SetActive(true);
        }
        else if (index == 7)
        {
            // todo stop wave manager

            // TODO draw attention to lower biomass
        }
        else if (index == 8)
        {
            locked = true;
            Instantiate(possum, new Vector3(5, 24, 0), Quaternion.identity);
            instantiatedFrame = Instantiate(highlightFrame, new Vector3Int(5, 24, 0), Quaternion.identity);
            _resourceManager.ReFindBiomass();
        }
        else
        {
//            instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer = true;
        }
    }

    private void CheckUnlockCondition()
    {
        if (index == 2)
        {
            // if it was locked last frame, and now isn't, then the player did what they needed to do.
            // so play the next line automatically
            if (instantiatedFrame != null)
            {
                if (locked == instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer)
                {
                    locked = false;
                    Destroy(instantiatedFrame);
                    StopAllCoroutines();
                    tmp.text = lines[index];
                    NextLine();
                }
            }
        }

        else if (index == 3)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                locked = false;
                StopAllCoroutines();
                tmp.text = lines[index];
                NextLine();
            }
        }

        // todo bug if they complete this step in 4
        else if (index == 5)
        {
            if (_resourceManager.towers.First().GetComponent<TowerAttack2>().connected)
            {
                Debug.Log("CONNECTED TOWER");
                if (instantiatedFrame != null)
                {
                    locked = false;
                    Destroy(instantiatedFrame);
                    StopAllCoroutines();
                    tmp.text = lines[index];
                    NextLine();
                }
            }
        }

        else if (index == 8)
        {
            if (_resourceManager.biomassRate > 0)
            {
                locked = false;
                Destroy(instantiatedFrame);
                StopAllCoroutines();
                tmp.text = lines[index];
                NextLine();
            }
        }
    }
}
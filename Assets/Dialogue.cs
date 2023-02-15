using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

// https://youtu.be/8oTYabhj248
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

    // Start is called before the first frame update
    void Start()
    {
        //tilemap = GameObject.Find("Grid").transform.GetChild(0).gameObject.GetComponent<Tilemap>();
        tmp.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
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
            instantiatedFrame = Instantiate(highlightFrame, new Vector3Int(12, 24, 0), Quaternion.identity);
            instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer = false;
            locked = true;
        }
        else
        {
//            instantiatedFrame.GetComponent<HighlightCollision>().touchedPlayer = true;
        }
    }
}
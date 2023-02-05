using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeHealth : MonoBehaviour
{
    public TreeManager tree;
    public AudioClip lowHealthSFX;

    private GameObject character;
    private GameObject[] leafs;
    private int leafNum;
    private bool soundPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("character");
        leafs = GameObject.FindGameObjectsWithTag("leaf");
        leafNum = leafs.Length;
    }

    // Update is called once per frame
    void Update()
    {
        int curHealth = (int)tree.health / 10;
        //Debug.Log("curhealth: " + curHealth);
        for(int i = 0; i < leafs.Length; i++)
        {
            if (i >= leafs.Length - curHealth)
                leafs[i].SetActive(true);
            else
                leafs[i].SetActive(false);
        }
        leafNum = curHealth;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeHealth : MonoBehaviour
{
    public TreeManager tree;
    private GameObject[] leafs;
    private int leafNum;
    // Start is called before the first frame update
    void Start()
    {
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

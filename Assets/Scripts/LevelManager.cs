using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;

    public TreeManager tree;

    public timerScript timer;

    ResourceManager resourceManager;

    void Start()
    {
        tree = GameObject.Find("treeHouseFull").GetComponent<TreeManager>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tree.health <= 0)
        {
            isGameOver = true;
            LevelLost();
        }
    }

    void LevelLost()
    {
        SceneManager.LoadScene("LoseScene");
    }

    void LevelWon()
    {
        SceneManager.LoadScene("WinScene");
    }
}
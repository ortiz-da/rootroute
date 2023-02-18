using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// USES https://youtu.be/CE9VOZivb3I
public class LevelManager : MonoBehaviour
{
    public TreeManager tree;

    public Animator transition;

    public float transitionTime = 1f;

    void Start()
    {
        tree = GameObject.Find("treeHouseFull").GetComponent<TreeManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LevelLost()
    {
        SceneManager.LoadScene("LoseScene");
    }

    void LevelWon()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
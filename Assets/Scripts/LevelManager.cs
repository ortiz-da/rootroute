using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// USES https://youtu.be/CE9VOZivb3I
public class LevelManager : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;


    void Start()
    {
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

    public void LoadLevelByName(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevel(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}
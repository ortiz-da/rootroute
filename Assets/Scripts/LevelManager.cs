using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// USES https://youtu.be/CE9VOZivb3I
public class LevelManager : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    private static readonly int Start1 = Animator.StringToHash("Start");

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LevelLost()
    {
        LoadScene("LoseScene");
    }

    void LevelWon()
    {
        LoadScene("WinScene");
    }

    // kinda bad naming, since it's the same as SceneManager.LoadScene, but does fading.
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneCoroutine(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        transition.SetTrigger(Start1);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        transition.SetTrigger(Start1);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
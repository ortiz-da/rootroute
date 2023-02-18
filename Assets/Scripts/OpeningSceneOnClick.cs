using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneOnClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Save stuff
            SceneManager.LoadScene("LoadingScene");
            Debug.Log("This is a log message.");
        }
    }
}
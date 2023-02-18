using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnSpace : MonoBehaviour
{
    private string activeSceneName;

    private LevelManager _levelManager;

    public string sceneToLoad;


    // Start is called before the first frame update
    void Start()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        activeSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            !(activeSceneName.Equals("MainScene") || activeSceneName.Equals("TutorialScene")))
        {
            _levelManager.LoadLevelByName(sceneToLoad);
        }
    }
}
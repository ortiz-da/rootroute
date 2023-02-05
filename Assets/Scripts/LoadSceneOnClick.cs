using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            //Save stuff
            SceneManager.LoadScene("SampleScene");
            Debug.Log("This is a log message.");
        }
    }
}
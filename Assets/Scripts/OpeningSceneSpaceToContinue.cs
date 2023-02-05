using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningSceneSpaceToContinue : MonoBehaviour
{
    void UpdateLoadScene() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            //Save stuff
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
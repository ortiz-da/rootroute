using UnityEngine;

public class SpaceNextScene : MonoBehaviour
{
    private LevelManager _levelManager;
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _levelManager.LoadScene(sceneToLoad);
        }
    }
}
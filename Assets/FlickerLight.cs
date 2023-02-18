using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerLight : MonoBehaviour
{
    private Light2D _light;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light2D>();
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            _light.intensity = Random.Range(.5f, 1f);
            yield return new WaitForSeconds(Random.Range(.01f, .05f));
        }
    }
}
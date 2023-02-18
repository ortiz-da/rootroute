using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerLight : MonoBehaviour
{
    private Light2D _light;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light2D>();
        StartCoroutine(flicker());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator flicker()
    {
        while (true)
        {
            _light.intensity = Random.Range(.5f, 1f);
            yield return new WaitForSeconds(Random.Range(.01f, .05f));
        }
    }
}
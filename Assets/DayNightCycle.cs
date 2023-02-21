using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class DayNightCycle : MonoBehaviour
{
    private RunWaves _runWaves;

    private Light2D _skyLight;

    public GameObject star;

    private List<GameObject> stars = new List<GameObject>();

    public SpriteRenderer skyBackdrop;
    public Sprite daySky;
    public Sprite nightSky;
    public Sprite eveningSky;
    public Sprite morningsky;


    enum TimeOfDay
    {
        Night,
        Evening,
        Morning,
        Day
    }

    [SerializeField] private TimeOfDay _timeOfDay = TimeOfDay.Evening;

    // Start is called before the first frame update
    void Start()
    {
        _runWaves = GameObject.Find("WaveManager").GetComponent<RunWaves>();
        _skyLight = GameObject.Find("SkyLight").GetComponent<Light2D>();

        // start off at the right intensity
        float endIntensity = (float)_timeOfDay / Enum.GetNames(typeof(TimeOfDay)).Length;
        StartCoroutine(LerpToNextTime(endIntensity, 5f));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AdvanceTime()
    {
        switch (_timeOfDay)
        {
            case TimeOfDay.Night:
                _timeOfDay = TimeOfDay.Morning;
                skyBackdrop.sprite = morningsky;
                DeleteStars();
                break;

            case TimeOfDay.Evening:
                _timeOfDay = TimeOfDay.Night;
                skyBackdrop.sprite = nightSky;

                MakeStars();
                break;

            case TimeOfDay.Morning:
                _timeOfDay = TimeOfDay.Day;
                skyBackdrop.sprite = daySky;

                break;

            case TimeOfDay.Day:
                _timeOfDay = TimeOfDay.Evening;
                skyBackdrop.sprite = eveningSky;

                break;
        }
        // https://stackoverflow.com/a/856165

        float endIntensity = (float)_timeOfDay / Enum.GetNames(typeof(TimeOfDay)).Length;
        StartCoroutine(LerpToNextTime(endIntensity, 5f));
    }

    // https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/
    IEnumerator LerpToNextTime(float endValue, float duration)
    {
        // so that night isn't pitch black
        if (endValue == 0)
        {
            endValue = .1f;
        }

        float time = 0;
        float startValue = _skyLight.intensity;
        while (time < duration)
        {
            _skyLight.intensity = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        _skyLight.intensity = endValue;
    }

    private void MakeStars()
    {
        // between y 26 and 34
        // todo remove magic numbers
        for (int i = 0; i < 50; i++)
        {
            // so they don't spawn on tree
            float[] starX = { Random.Range(0f, 11f), Random.Range(20f, VariableSetup.worldXSize) };


            GameObject tempStar = Instantiate(star,
                new Vector3(starX[Random.Range(0, 2)], Random.Range(27f, 34f), 1),
                Quaternion.identity);
            stars.Add(tempStar);
            StartCoroutine(delay());
        }
    }

    private void DeleteStars()
    {
        foreach (var aStar in stars)
        {
            Destroy(aStar);
            StartCoroutine(delay());
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(Random.Range(1, 2f));
    }
}
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float countdown;
    public TextMeshProUGUI timerText;

    void Start()
    {
        countdown = VariableSetup.countdownTimerLength;
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        UpdateTimer();
    }

    void UpdateTimer()
    {
        int sec = (int)countdown % 60;
        int min = (int)countdown / 60;
        timerText.text = min.ToString() + ":" + sec.ToString();
    }
}
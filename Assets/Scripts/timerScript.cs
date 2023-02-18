using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timerScript : MonoBehaviour
{
    public float countdown;
    public TextMeshProUGUI timerText;
    void Start()
    {
        countdown = VariableSetup.countdownTimerLength;
        updateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime; 
        updateTimer();
    }

    void updateTimer()
    {
        int sec = (int)countdown % 60;
        int min = (int)countdown / 60;
        timerText.text = min.ToString() + ":" + sec.ToString();
    }
}

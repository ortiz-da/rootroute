using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicPlayer : MonoBehaviour
{
    public AudioSource combatAudioSource;
    public AudioSource healthLowAudioSource;
    public AudioSource overgroundAudioSource;
    public AudioSource undergroundAudioSource;


    public AudioClip combatClip;
    public AudioClip healthLowClip;
    public AudioClip overgroundClip;
    public AudioClip undegroundClip;
    
    // Start is called before the first frame update
    void Start()
    {
        combatAudioSource.clip = combatClip;
        combatAudioSource.volume = 0;
        combatAudioSource.Play();
        healthLowAudioSource.clip = healthLowClip;
        combatAudioSource.volume = 0;
        healthLowAudioSource.Play();
        overgroundAudioSource.clip = overgroundClip;
        overgroundAudioSource.Play();
        undergroundAudioSource.clip = undegroundClip;
        undergroundAudioSource.Play();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

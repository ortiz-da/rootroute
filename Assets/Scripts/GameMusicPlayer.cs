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


    public GameObject player;

    public TreeManager tree;

    private float playerHeight = 4.4f;
    // 4 to -20 is the world height

    // Start is called before the first frame update
    private void Start()
    {
        combatAudioSource.clip = combatClip;
        combatAudioSource.volume = 0;
        combatAudioSource.Play();
        healthLowAudioSource.clip = healthLowClip;
        healthLowAudioSource.volume = 0;
        healthLowAudioSource.Play();
        overgroundAudioSource.clip = overgroundClip;
        overgroundAudioSource.Play();
        undergroundAudioSource.clip = undegroundClip;
        undergroundAudioSource.Play();
    }

    // Update is called once per frame
    private void Update()
    {
        playerHeight = Mathf.Abs(player.transform.position.y - 4.4f);
        undergroundAudioSource.volume = playerHeight / 21;
        overgroundAudioSource.volume = 1 - playerHeight / 21;

        healthLowAudioSource.volume = 1f - tree.health / tree.maxHealth;
        combatAudioSource.volume = tree.enemyCounter / 10f;
    }
}
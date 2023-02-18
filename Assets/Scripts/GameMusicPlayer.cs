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


    private float playerHeight;
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
        // Fade between above and below ground music
        // Could be cleaned up
        // 1.5 for the 1 block of grass + a bit above.
        playerHeight = Mathf.Abs(1 - player.transform.position.y / (VariableSetup.worldYSize + 1.5f));
        undergroundAudioSource.volume = playerHeight;
        overgroundAudioSource.volume = 1 - playerHeight;

        // Change music if tree health is low, or if tree is being attacked
        healthLowAudioSource.volume = 1f - tree.health / tree.maxHealth;
        combatAudioSource.volume = tree.enemyCounter / 10f;
    }
}
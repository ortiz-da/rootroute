using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public TreeHealth healthBar;

    public int enemyCounter;

    public AudioClip lowHealthSound;

    private AudioSource _audioSource;

    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        maxHealth = VariableSetup.treeLife;
        health = VariableSetup.treeLife;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        // todo fix
        if (health <= 15)
        {
            _audioSource.clip = lowHealthSound;
            _audioSource.Play();
        }



        if (health <= 0)
        {
            _levelManager.LevelLost();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("AGEnemy"))
        {
            enemyCounter++;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("AGEnemy")) enemyCounter--;
    }

    public void DecreaseHealth(int dmg)
    {
        this.health -= dmg;
    }
}
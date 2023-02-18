using System.Collections;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public TreeHealth healthBar;

    [SerializeField] private AGEnemyMovement AGEnemy;
    [SerializeField] private bool isBeingAttacked;
    public int enemyCounter;

    public AudioClip lowHealthSound;

    private AudioSource audioSource;

    private void Start()
    {
        maxHealth = VariableSetup.treeLife;
        health = VariableSetup.treeLife;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        // todo fix
        if (health <= 15)
        {
            audioSource.clip = lowHealthSound;
            audioSource.Play();
        }

        if (enemyCounter <= 0)
        {
            isBeingAttacked = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("AGEnemy"))
        {
            enemyCounter++;
            isBeingAttacked = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("AGEnemy")) enemyCounter--;
    }

    public void decreaseHealth(int dmg)
    {
        this.health -= dmg;
    }
}
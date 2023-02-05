using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public TreeHealth healthBar;

    [SerializeField] private AGEnemyMovement AGEnemy;
    [SerializeField] private bool isBeingAttacked = false;
    [SerializeField] private int enemyCounter = 0;
    
    private AudioSource audioSource;

    public AudioClip lowHealthSound;
    void Start()
    {
        maxHealth = VariableSetup.treeLife;
        health = VariableSetup.treeLife;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingAttacked)
        {
            StartCoroutine(AGAttacking());
        }
        Debug.Log(health);
        Debug.Log(isBeingAttacked);
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
        if (col.gameObject.CompareTag("AGEnemy"))
        {
            enemyCounter--;
        }
        
    }

    IEnumerator AGAttacking()
    {
        health --;
        if (health <= 15)
        {
            audioSource.clip = lowHealthSound;
            audioSource.Play();
        }
        isBeingAttacked = false;
        yield return new WaitForSeconds(VariableSetup.beetleAttackRate);

        if(enemyCounter > 0)
        {
            isBeingAttacked = true;
        }
        else
        {
            isBeingAttacked=false;
        }

    }
}

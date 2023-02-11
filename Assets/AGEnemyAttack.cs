using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGEnemyAttack : MonoBehaviour
{
    public int damage = 1;

    public TreeManager treeManager;

    private AudioSource audioSource;
    public AudioClip crunchSound;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        treeManager = GameObject.FindGameObjectWithTag("tree").GetComponent<TreeManager>();
    }

    // Called via animation event in the TermiteAttackAnimation
    public void Attack()
    {
        treeManager.decreaseHealth(damage);
        //Debug.Log("PLAY CRUNCH");
        audioSource.clip = crunchSound;
        audioSource.pitch = Random.Range(.5f, 1f);
        audioSource.Play();
    }
}
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
    void Start()
    {
        maxHealth = VariableSetup.treeLife;
        health = VariableSetup.treeLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingAttacked)
        {
            StartCoroutine(AGAttacking());
        }
        //Debug.Log(health);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("AGEnemy"))
        {
            isBeingAttacked = true;
        }
    }

    IEnumerator AGAttacking()
    {
        health --;
        isBeingAttacked = false;
        yield return new WaitForSeconds(VariableSetup.beetleAttackRate);
        isBeingAttacked = true;
    }
}

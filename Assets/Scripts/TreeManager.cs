using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public TreeHealth healthBar;
    [SerializeField] private VariableSetup masterVariables;
    [SerializeField] private AGEnemyMovement AGEnemy;
    [SerializeField] private float timePassed = 0f;
    [SerializeField] private bool isBeingAttacked = false;
    void Start()
    {
        maxHealth = masterVariables.treeLife;
        health = masterVariables.treeLife;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = timePassed + Time.deltaTime;
        //Debug.Log((int)timePassed);

        if (isBeingAttacked)
        {
            AGAttacking();
        }
        Debug.Log(health);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("AGEnemy"))
        {
            isBeingAttacked = true;
        }
    }

    void AGAttacking()
    {
        if (((int)timePassed % masterVariables.beetleAttackRate) == 0)
        {
            health = health - 1;
        }
    }
}

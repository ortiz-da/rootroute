using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public TreeHealth healthBar;
    [SerializeField] private AGEnemyMovement AGEnemy;
    [SerializeField] private float timePassed = 0f;
    [SerializeField] private int timeInt = 0;
    [SerializeField] private bool isBeingAttacked = false;
    void Start()
    {
        maxHealth = VariableSetup.treeLife;
        health = VariableSetup.treeLife;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = timePassed + Time.deltaTime;
        timeInt = (int)timePassed;
        //Debug.Log((int)timePassed);

        if (isBeingAttacked && ((timeInt % VariableSetup.beetleAttackRate) == 0))
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
            health --;
    }
}

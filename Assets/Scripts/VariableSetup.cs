using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableSetup : MonoBehaviour
{
    public float treeLife = 100f;
    public float biomass = 100f;

    //rates in seconds, i.e. seconds between events
    public float wormSpawnRate = 120f;

    public float beetleSpawnRate = 60f;

    public float countdownTimerLength = 180f;

    public float beetleHealth = 10f;

    public float tower1Cost = 50f;
    public float tower1Dmg = 5f;
    public float tower1Health = 20f;
    public float tower1BiomassPerShot = 1f;


    public float myceliumHP = 10f;

    public float wormHP = 10f;

    public float playerAtkDmg = 1f;

    public float wormAttackRate = 3f;
    public int beetleAttackRate = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

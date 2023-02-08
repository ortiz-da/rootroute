using UnityEngine;

public class VariableSetup : MonoBehaviour
{
    public static float treeLife = 100f;
    public static float startingBiomass = 50f;

    //rates in seconds, i.e. seconds between events
    public static float wormSpawnRate = 120f;

    public static float beetleSpawnRate = 60f;

    public static float countdownTimerLength = 240f;

    public static float beetleHealth = 10f;

    public static float tower1Cost = 25f;
    public static float tower1Dmg = 5f;
    public static float tower1Health = 20f;
    public static float tower1BiomassPerShot = 1f;

    public static float rate = 2f;

    public static float myceliumHP = 10f;

    public static float wormHP = 10f;

    public static float playerAtkDmg = 1f;

    public static float wormAttackRate = 3f;

    public static int beetleAttackRate = 2;

    public static int biomassLife = 3;

    public static int numWaves = 15;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
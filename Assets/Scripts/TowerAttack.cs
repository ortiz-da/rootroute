    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private GameObject beetle;
    public GameObject bullet;
    [SerializeField] private float distanceToBeetle;

    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        beetle = GameObject.FindGameObjectWithTag("AGEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(attacking)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attacking = false;
    }

    IEnumerator attack()
    {

    }
}

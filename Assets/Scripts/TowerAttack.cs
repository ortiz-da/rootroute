using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private GameObject beetle;
    public GameObject bullet;
    [SerializeField] private float distanceToBeetle;

    // Start is called before the first frame update
    void Start()
    {
        beetle = GameObject.FindGameObjectWithTag("AGEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToBeetle = Vector3.Distance(transform.position, beetle.transform.position);
        Debug.Log(distanceToBeetle);
    }
}

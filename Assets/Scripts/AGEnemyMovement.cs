using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class AGEnemyMovement : MonoBehaviour
{
    [SerializeField] private float beetleSpeed = 0.1f;
    [SerializeField] private float treeOffset = 1f;
    [SerializeField] private bool beetleAttacking = false;
    [SerializeField] private float timePassed = 0f;
    public GameObject character;
    public GameObject tree;
    [SerializeField] private VariableSetup masterVariables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = timePassed + Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(transform.position.x >= (tree.transform.position.x - treeOffset))
        {
            beetleSpeed = 0f;
            beetleAttacking = true;
        }

        transform.position += new Vector3(beetleSpeed, 0, 0);
        Physics2D.IgnoreCollision(character.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        /*
        if (beetleAttacking)
        {
            beetleAttack();
        }
        */
    }

    /*
    void beetleAttack()
    {
        if((timePassed % masterVariables.beetleAttackRate) == 1)
        {
            masterVariables.treeLife = masterVariables.treeLife - 1;
        }
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class AGEnemyMovement : MonoBehaviour
{
    [SerializeField] private float beetleSpeed = 0.1f;
    [SerializeField] private float timePassed = 0f;
    [SerializeField] private GameObject tree;

    private Animator animator;
    private static readonly int IsAttackingTree = Animator.StringToHash("IsAttackingTree");

    // Start is called before the first frame update
    void Start()
    {
        tree = GameObject.FindGameObjectWithTag("tree");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = timePassed + Time.deltaTime;
    }

    private void FixedUpdate()
    { 
        transform.position += new Vector3(beetleSpeed, 0, 0);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("tree"))
        {
            beetleSpeed = 0f;
            animator.SetBool(IsAttackingTree, true);
            
        }
        else
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}

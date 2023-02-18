using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class AGEnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 0.04f;
    [SerializeField] private GameObject tree;

    private Animator animator;
    private static readonly int IsAttackingTree = Animator.StringToHash("IsAttackingTree");
    private Vector3 treepos;


    // Start is called before the first frame update
    void Start()
    {
        tree = GameObject.FindGameObjectWithTag("tree");
        animator = GetComponent<Animator>();
        treepos = new Vector3(tree.transform.position.x, transform.position.y, tree.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, treepos, enemySpeed);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("tree"))
        {
            enemySpeed = 0f;
            animator.SetBool(IsAttackingTree, true);
        }
        else
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
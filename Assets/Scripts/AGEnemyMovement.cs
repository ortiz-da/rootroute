using UnityEngine;

public class AgEnemyMovement : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 0.04f;
    [SerializeField] private GameObject tree;

    private Animator _animator;
    private static readonly int IsAttackingTree = Animator.StringToHash("IsAttackingTree");
    private Vector3 _treepos;


    // Start is called before the first frame update
    void Start()
    {
        tree = GameObject.FindGameObjectWithTag("tree");
        _animator = GetComponent<Animator>();
        _treepos = new Vector3(tree.transform.position.x, transform.position.y, tree.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _treepos, enemySpeed);
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("tree"))
        {
            enemySpeed = 0f;
            _animator.SetBool(IsAttackingTree, true);
        }
        else
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private GameObject beetle;
    public GameObject bullet;
    private Animator animator;
    [SerializeField] private float distanceToBeetle;

    public GameObject particles;

    private bool isAttacking = false;

    private int attackersInTrigger = 0;

    public Collider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        beetle = GameObject.FindGameObjectWithTag("AGEnemy");
        animator = GetComponent<Animator>();

        GameObject hit = transform.GetChild(0).gameObject;
        hitbox = hit.GetComponent<Collider2D>();
        hitbox.enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("attacking", true);
        attackersInTrigger++;
        isAttacking= true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attackersInTrigger--;
        if (attackersInTrigger == 0)
        {
            isAttacking= false;
            animator.SetBool("attacking", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isAttacking)
        {
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        hitbox.enabled= true;
        isAttacking = false;
        yield return new WaitForSeconds(VariableSetup.rate);
        hitbox.enabled= false;
        isAttacking = true;
    }
}

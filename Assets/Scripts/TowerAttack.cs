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

    // Start is called before the first frame update
    void Start()
    {
        beetle = GameObject.FindGameObjectWithTag("AGEnemy");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackersInTrigger++;
        isAttacking= true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attackersInTrigger--;
        if (attackersInTrigger == 0)
        {
            isAttacking= false;
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
        animator.SetBool("attacking", true);
        Instantiate(particles, transform.position, Quaternion.identity);
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(VariableSetup.rate);
    }
}

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
        Debug.Log("Hit trigger");
        isAttacking= true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("attacking", false);
        StopAllCoroutines();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isAttacking)
        {
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        Instantiate(particles, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(VariableSetup.rate);
        isAttacking= false;
    }
}

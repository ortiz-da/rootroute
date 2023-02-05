    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerAttack : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float distanceToBeetle;

    public GameObject particles;

    private bool isAttacking = false;

    private int attackersInTrigger = 0;

    private ResourceManager resourceManager;
    private Tilemap tilemap;

    public Collider2D hitbox;

    public AudioClip towerAttackSFX; //add this to any script that needs to play a clip

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        resourceManager = GameObject.Find("treeHouseFull").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();

        GameObject hit = transform.GetChild(0).gameObject;
        hitbox = hit.GetComponent<Collider2D>();
        hitbox.enabled= false;
        resourceManager.towerPlaced(tilemap.WorldToCell(transform.position));
        Debug.Log(tilemap.WorldToCell(transform.position).ToString());
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
        AudioSource.PlayClipAtPoint(towerAttackSFX, transform.position);
        hitbox.enabled= false;
        isAttacking = true;
    }
}

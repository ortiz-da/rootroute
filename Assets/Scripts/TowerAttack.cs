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

    public Vector3Int position;
    public Vector2Int correctedPosition;

    private bool isAttacking = false;

    private int attackersInTrigger = 0;

    public ResourceManager resourceManager;
    private Tilemap tilemap;

    public Collider2D hitbox;
    
    public bool connected;
    
    private AudioSource audioSource;

    public AudioClip attackSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();

        GameObject hit = transform.GetChild(0).gameObject;
        hitbox = hit.GetComponent<Collider2D>();
        hitbox.enabled= false;

        position = tilemap.WorldToCell(transform.position);
        position.y -= 2; //2 or 3 here?

        resourceManager.towerPlaced(gameObject);

        correctedPosition = new Vector2Int();
        //Debug.Log("tower placed: " + tilemap.WorldToCell(transform.position).ToString());
        
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (connected)
        {
            animator.SetBool("attacking", true);
            attackersInTrigger++;
            isAttacking = true;
        }
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
        if (isAttacking && connected)
        {
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        if (connected)
        {
            if (!animator.GetBool("attacking"))
                animator.SetBool("attacking", true);
            Debug.Log("TOWER ATTACK");
            audioSource.clip = attackSound;
            audioSource.pitch = Random.Range(.5f, 1f);
            audioSource.Play();

            Instantiate(particles, transform.position, Quaternion.identity);
            hitbox.enabled = true;
            isAttacking = false;
            yield return new WaitForSeconds(VariableSetup.rate);

            hitbox.enabled = false;
            isAttacking = true;
        }
    }
}

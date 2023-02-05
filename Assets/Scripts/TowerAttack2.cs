using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TowerAttack2 : MonoBehaviour
{
    private Animator animator;
    public GameObject particles;
    
    public bool connected = true;
    private AudioSource audioSource;
    public AudioClip attackSound;

    public List<Collider2D> collidedEnemies = new List<Collider2D>();


    private bool shooting = false;
    
    public ResourceManager resourceManager;
    private Tilemap tilemap;
    
    public Vector3Int position;
    public Vector2Int correctedPosition;


    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        

        resourceManager.towerPlaced(gameObject);
        
        position = tilemap.WorldToCell(transform.position);
        position.y -= 2; //2 or 3 here?
        correctedPosition = new Vector2Int();

    }

    // Update is called once per frame
    void Update()
    {
        if (collidedEnemies.Count > 0 && !shooting && connected)
        {
            StartCoroutine(shoot());
        }

        if (collidedEnemies.Count == 0 || !shooting || !connected)
        {
            animator.SetBool("attacking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AGEnemy"))
        {
            collidedEnemies.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AGEnemy"))
        {
            collidedEnemies.Remove(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

    }

    IEnumerator shoot()
    {
        shooting = true;
        Instantiate(particles, transform.position, Quaternion.identity);
        
        resourceManager.biomassUpdate(-1);
        
        audioSource.clip = attackSound;
        audioSource.pitch = Random.Range(.5f, 1f);
        audioSource.Play();
        
        animator.SetBool("attacking", true);
        for (int i = 0; i < collidedEnemies.Count; i++)
        {
            https://forum.unity.com/threads/calling-function-from-other-scripts-c.57072/
            GameObject enemy = collidedEnemies[i].gameObject;
            enemy.GetComponent<BeetleHealth>().DecreaseHealth();
            // Only kills if health is 0
            enemy.GetComponent<BeetleHealth>().KillBeetle();
        }
        yield return new WaitForSeconds(1);
        shooting = false;

    }
    
}

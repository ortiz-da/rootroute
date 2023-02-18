using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TowerAttack2 : MonoBehaviour
{
    public GameObject particles;

    public bool connected = false;
    public AudioClip attackSound;

    public List<Collider2D> collidedEnemies = new();

    public ResourceManager resourceManager;

    public Vector3Int myceliumConnectorPosition;
    public Vector2Int correctedPosition;
    private Animator _animator;
    private AudioSource _audioSource;


    private bool _shooting;
    private Tilemap _tilemap;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        _tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();


        // resourceManager.towerPlaced(gameObject);

        // using awake so that these variables will be set before the resource manager attempts to pathfind from the 
        // myceliumConnectorPosition. Ensures that if a tower is placed on a connected mycelium, the tower will come into the world powered.
        myceliumConnectorPosition = _tilemap.WorldToCell(transform.position);
        myceliumConnectorPosition.y -= 2; //2 or 3 here?
        correctedPosition = new Vector2Int();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (collidedEnemies.Count > 0 && !_shooting && connected)
        {
            // TODO in progress, want to make display of rate reflect when towers are shooting
            //resourceManager.biomassRate -= VariableSetup.towerSecBetweenShots;
            StartCoroutine(Shoot());
        }

        if (collidedEnemies.Count == 0 || !_shooting || !connected) _animator.SetBool("attacking", false);

        if (_animator.GetBool("powered") != connected)
        {
            _animator.SetBool("powered", connected);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AGEnemy")) collidedEnemies.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AGEnemy")) collidedEnemies.Remove(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
    }

    private IEnumerator Shoot()
    {
        if (resourceManager.biomass > 0)
        {
            _shooting = true;
            Instantiate(particles, transform.position, Quaternion.identity);

            resourceManager.BiomassUpdate(-1);

            _audioSource.clip = attackSound;
            _audioSource.pitch = Random.Range(.5f, 1f);
            _audioSource.Play();

            _animator.SetBool("attacking", true);
            for (var i = 0; i < collidedEnemies.Count; i++)
            {
                var enemy = collidedEnemies[i].gameObject;
                enemy.GetComponent<BeetleHealth>().DecreaseHealth();
                // Only kills if health is 0
                // todo clean up this code
                enemy.GetComponent<BeetleHealth>().KillBeetle();
            }

            yield return new WaitForSeconds(1);
            _shooting = false;
        }
    }

    public void ConnectTower()
    {
        connected = true;
        _animator.SetBool("powered", true);
    }

    public void DisconnectTower()
    {
        connected = false;
        _animator.SetBool("powered", false);
    }
}
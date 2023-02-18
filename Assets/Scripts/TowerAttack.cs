using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerAttack : MonoBehaviour
{
    [SerializeField] private float distanceToBeetle;

    public GameObject particles;

    public Vector3Int position;
    public Vector2Int correctedPosition;

    public ResourceManager resourceManager;

    public Collider2D hitbox;

    public bool connected;

    public AudioClip attackSound;
    private Animator _animator;

    private int _attackersInTrigger;

    private AudioSource _audioSource;

    private bool _isAttacking;
    private Tilemap _tilemap;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        _tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();

        var hit = transform.GetChild(0).gameObject;
        hitbox = hit.GetComponent<Collider2D>();
        hitbox.enabled = false;

        position = _tilemap.WorldToCell(transform.position);
        position.y -= 2; //2 or 3 here?

        // resourceManager.towerPlaced(gameObject);

        correctedPosition = new Vector2Int();
        //Debug.Log("tower placed: " + tilemap.WorldToCell(transform.position).ToString());

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (connected)
        {
            _animator.SetBool("attacking", true);
            _attackersInTrigger++;
            _isAttacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _attackersInTrigger--;
        if (_attackersInTrigger == 0)
        {
            _isAttacking = false;
            _animator.SetBool("attacking", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isAttacking && connected) StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        if (connected)
        {
            if (!_animator.GetBool("attacking"))
                _animator.SetBool("attacking", true);
            Debug.Log("TOWER ATTACK");
            _audioSource.clip = attackSound;
            _audioSource.pitch = Random.Range(.5f, 1f);
            _audioSource.Play();

            Instantiate(particles, transform.position, Quaternion.identity);
            hitbox.enabled = true;
            _isAttacking = false;
            yield return new WaitForSeconds(VariableSetup.rate);

            hitbox.enabled = false;
            _isAttacking = true;
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LayTower : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject tower1;

    private ResourceManager _resourceManager;

    private TextMeshProUGUI _errorText;

    private Collider2D _grassTrigger;

    private HighlightBlock _currentGrass;

    void Start()
    {
        _resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();

        _errorText = GameObject.Find("errorText").GetComponent<TextMeshProUGUI>();

        _grassTrigger = gameObject.GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _currentGrass != null)
        {
            if (!_currentGrass.hasTower)
            {
                _currentGrass.PlaceTower();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("grass"))
        {
            _currentGrass = collision.gameObject.GetComponent<HighlightBlock>();
        }
        else
            _currentGrass = null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _currentGrass = null;
    }
}
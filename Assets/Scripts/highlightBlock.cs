using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightBlock : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    public Tilemap tilemap;

    public bool hasTower;

    public GameObject tower1;

    public TileBase mineshaftWithMyceliumTile;

    private Color _blockColor;

    private TextMeshProUGUI _errorText;

    private ResourceManager _resourceManager;

    private void Start()
    {
        // http://answers.unity.com/answers/993502/view.html
        _resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();

        _errorText = GameObject.Find("errorText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("character") && !hasTower)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _blockColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color =
                new Color(_blockColor.r, _blockColor.g, _blockColor.b, .5f);
        }
    }


    /*private void OnTriggerStay2D(Collider2D other)
    {
        
        if (Input.GetKeyDown(KeyCode.F) && !hasTower && resourceManager.biomass >= VariableSetup.tower1Cost)
        {
            Vector3 towerPos = new Vector3(this.transform.position.x, this.transform.position.y + 1.5f);
            Instantiate(tower1, towerPos, Quaternion.identity, null);

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;
            Vector3Int buildBlock = tilemap.WorldToCell(transform.position);
            tilemap.SetTile(buildBlock, mineshaftWithMyceliumTile);

             // Debug.Log("PLACE TOWER");
            hasTower = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(blockColor.r, blockColor.g, blockColor.b, 1f);
            
        }
        else if(Input.GetKeyDown(KeyCode.F) && resourceManager.biomass < VariableSetup.tower1Cost)
        {
            StartCoroutine(textDisplay());
        }
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            // Debug.Log("LEAVE");

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _blockColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color =
                new Color(_blockColor.r, _blockColor.g, _blockColor.b, 1f);
        }
    }

    public void PlaceTower()
    {
        if (_resourceManager.biomass >= VariableSetup.tower1Cost)
        {
            var towerPos = new Vector3(transform.position.x, transform.position.y + 1.5f);
            var instatiatedTower = Instantiate(tower1, towerPos, Quaternion.identity, null);

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;
            var buildBlock = tilemap.WorldToCell(transform.position);
            tilemap.SetTile(buildBlock, mineshaftWithMyceliumTile);

            // Debug.Log("PLACE TOWER");
            hasTower = true;
            gameObject.GetComponent<SpriteRenderer>().color =
                new Color(_blockColor.r, _blockColor.g, _blockColor.b, 1f);

            // tell the boolean grid there has been a tower placed
            _resourceManager.TowerPlaced(instatiatedTower);
        }
        else if (_resourceManager.biomass < VariableSetup.tower1Cost)
        {
            StartCoroutine(TextDisplay());
        }
    }

    private IEnumerator TextDisplay()
    {
        _errorText.text = "Not enough biomatter!";
        yield return new WaitForSeconds(1);
        _errorText.text = "";
    }
}
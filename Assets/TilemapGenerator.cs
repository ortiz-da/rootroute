using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public TileBase dirtTile;
    public TileBase bedrockTile;
    public TileBase myceliumTile;

    private Tilemap _tilemap;

    // Start is called before the first frame update
    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        _tilemap.ClearAllTiles();
        for (var y = 0; y < VariableSetup.worldYSize; y++)
        for (var x = 0; x < VariableSetup.worldXSize; x++)
        {
            var rand = Random.Range(0, 10);
            _tilemap.SetTile(new Vector3Int(x, y), rand > 0 || y == 24 ? dirtTile : bedrockTile);
        }

        // put down the origin block
        var originX = _tilemap.size.x / 2;
        var originY = _tilemap.size.y - 1;
        _tilemap.SetTile(new Vector3Int(originX, originY), myceliumTile);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
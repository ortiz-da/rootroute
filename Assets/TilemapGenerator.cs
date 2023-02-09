using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{

    private Tilemap _tilemap;

    public TileBase dirtTile;
    public TileBase bedrockTile;

    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        for (int y = 0; y >= -25; y--)
        {
            for (int x = -16; x <= 16; x++)
            {
                int rand = Random.Range(0, 10);
                _tilemap.SetTile(new Vector3Int(x, y), rand > 0 || y == 0 ? dirtTile : bedrockTile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

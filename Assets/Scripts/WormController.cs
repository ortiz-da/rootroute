using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WormController : MonoBehaviour
{
    public GameObject worm;

    public Tilemap tilemap;

    Rigidbody2D wormRigid;

    private float wormSpeed = 0.1f;

    WormDetector detector;
    void Start()
    {
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        wormRigid = GetComponent<Rigidbody2D>();
        turnWorm();
    }

    // Update is called once per frame
    void Update()
    {
        wormRigid.velocity = transform.forward * wormSpeed;
        Vector3Int intPos = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        Debug.Log(tilemap.GetTile(intPos));
    }

    public void turnWorm()
    {
        transform.Rotate(0, 0, Random.Range(1, 360));
    }
}

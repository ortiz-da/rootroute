using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used in tutorial to see if player collides with "highlight" box
public class HighlightCollision : MonoBehaviour
{
    public bool touchedPlayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("character"))
        {
            touchedPlayer = true;
        }
    }
}
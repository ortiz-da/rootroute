using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// USING: https://youtu.be/28JTTXqMvOU
public class MinimapScript : MonoBehaviour
{

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.z = -1;

        transform.position = newPosition;
    }
}

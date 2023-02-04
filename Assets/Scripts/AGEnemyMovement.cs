using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class AGEnemyMovement : MonoBehaviour
{
    [SerializeField] private float beetleSpeed = 0.1f;
    [SerializeField] private float timePassed = 0f;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject AGPrefab;

    // Start is called before the first frame update
    void Start()
    {
        tree = GameObject.FindGameObjectWithTag("tree");
        character = GameObject.FindGameObjectWithTag("character");
        AGPrefab = GameObject.FindGameObjectWithTag("AGEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = timePassed + Time.deltaTime;
    }

    private void FixedUpdate()
    { 
        transform.position += new Vector3(beetleSpeed, 0, 0);
        Physics2D.IgnoreCollision(character.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(AGPrefab.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("tree"))
        {
            beetleSpeed = 0f;
        }
    }
}

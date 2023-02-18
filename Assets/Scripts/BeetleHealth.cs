using UnityEngine;

public class BeetleHealth : MonoBehaviour
{
    // Start is called before the first frame update
    float _health = VariableSetup.beetleHealth;

    void Start()
    {
        // Debug.Log("beetle start");
        //Debug.Log(name+ ": " + health); 
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("attackBox"))
        {
            
            health -= VariableSetup.tower1Dmg;
            Debug.Log(name + " collision! health: " + health);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    */

    // NEW VERSION
    public void DecreaseHealth()
    {
        _health -= VariableSetup.tower1Dmg;
    }

    public void KillBeetle()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
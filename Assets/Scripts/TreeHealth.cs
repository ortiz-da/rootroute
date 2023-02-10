using UnityEngine;

public class TreeHealth : MonoBehaviour
{
    public TreeManager tree;
    public AudioClip lowHealthSFX;

    private GameObject character;
    private int leafNum;

    private GameObject[] leafs;

    // Start is called before the first frame update
    private void Start()
    {
        character = GameObject.Find("character");
        leafs = GameObject.FindGameObjectsWithTag("leaf");
        leafNum = leafs.Length;
    }

    // Update is called once per frame
    private void Update()
    {
        var curHealth = (int)tree.health / 10;
        //Debug.Log("curhealth: " + curHealth);
        for (var i = 0; i < leafs.Length; i++)
            if (i >= leafs.Length - curHealth)
                leafs[i].SetActive(true);
            else
                leafs[i].SetActive(false);
        leafNum = curHealth;
    }
}
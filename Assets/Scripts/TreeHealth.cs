using UnityEngine;

public class TreeHealth : MonoBehaviour
{
    public TreeManager tree;
    public AudioClip lowHealthSfx;

    private GameObject _character;
    private int _leafNum;

    private GameObject[] _leafs;

    // Start is called before the first frame update
    private void Start()
    {
        _character = GameObject.Find("character");
        _leafs = GameObject.FindGameObjectsWithTag("leaf");
        _leafNum = _leafs.Length;
    }

    // Update is called once per frame
    private void Update()
    {
        var curHealth = (int)tree.health / 10;
        //Debug.Log("curhealth: " + curHealth);
        for (var i = 0; i < _leafs.Length; i++)
            if (i >= _leafs.Length - curHealth)
                _leafs[i].SetActive(true);
            else
                _leafs[i].SetActive(false);
        _leafNum = curHealth;
    }
}
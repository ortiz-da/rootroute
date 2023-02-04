using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeHealth : MonoBehaviour
{
    public Image healthImage;
    private TreeManager tree;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = Mathf.Clamp(tree.health / tree.maxHealth, 0f, 1f);
    }
}

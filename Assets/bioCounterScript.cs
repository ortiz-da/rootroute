using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bioCounterScript : MonoBehaviour
{
    public TextMeshProUGUI bioText;
    public Image rateImage;
    public TextMeshProUGUI rateText;

    ResourceManager resourceManager;
    float curbio;
    float currate;
    void Start()
    {
        bioText = GameObject.Find("bioCounter").GetComponent<TextMeshProUGUI>();
        // rateImage = GameObject.Find("rate").GetComponent<Image>();
            //rateText = GameObject.Find("rate#").GetComponent<TextMeshProUGUI>();

        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        updateBio();
    }

    // Update is called once per frame
    void Update()
    {
        if(resourceManager.biomass != curbio)
        {
            updateBio();
        }
    }



    private void updateBio()
    {
        curbio = resourceManager.biomass;
        bioText.text = "Biomatter: " + curbio;
    }
    
}

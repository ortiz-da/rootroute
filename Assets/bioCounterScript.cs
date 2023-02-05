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
        rateImage = GameObject.Find("rate").GetComponent<Image>();
        rateText = GameObject.Find("rate#").GetComponent<TextMeshProUGUI>();

        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        updateBio();
        updateRate();

    }

    // Update is called once per frame
    void Update()
    {
        if(resourceManager.biomass != curbio)
        {
            updateBio();
        }
        if(resourceManager.biomassRate!= currate)
        {
            updateRate();
        }
    }

    Color colorDetermine(float rate)
    {
        if(rate < 1 && rate > -1)
        {
            return Color.yellow;
        }
        else if(rate > 1) 
        { 
            return Color.green;
        }
        else
        {
            return Color.red;
        }
    }

    private void updateBio()
    {
        curbio = resourceManager.biomass;
        bioText.text = "Biomatter: " + curbio;
    }

    private void updateRate()
    {
        currate = resourceManager.biomassRate;
        rateText.text = "+" + currate;
        rateImage.color = colorDetermine(currate);
    }
}

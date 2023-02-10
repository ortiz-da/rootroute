using TMPro;
using UnityEngine;

public class bioCounterScript : MonoBehaviour
{
    public TextMeshProUGUI bioText;
    private float curbio;
    private float currate;

    private ResourceManager resourceManager;

    private void Start()
    {
        bioText = GameObject.Find("bioCounter").GetComponent<TextMeshProUGUI>();
        // rateImage = GameObject.Find("rate").GetComponent<Image>();
        //rateText = GameObject.Find("rate#").GetComponent<TextMeshProUGUI>();

        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        updateBio();
    }

    // Update is called once per frame
    private void Update()
    {
        if (resourceManager.biomass != curbio) updateBio();
    }


    private void updateBio()
    {
        curbio = resourceManager.biomass;
        bioText.text = "Biomatter: " + curbio;
    }
}
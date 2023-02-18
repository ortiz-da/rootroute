using TMPro;
using UnityEngine;

public class BioCounterScript : MonoBehaviour
{
    private TextMeshProUGUI _bioText;
    private float _curbio;
    private float _currate;

    private ResourceManager _resourceManager;

    private void Start()
    {
        _bioText = GetComponent<TextMeshProUGUI>();
        // rateImage = GameObject.Find("rate").GetComponent<Image>();
        //rateText = GameObject.Find("rate#").GetComponent<TextMeshProUGUI>();

        _resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        UpdateBio();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_resourceManager.biomass != _curbio) UpdateBio();
    }


    private void UpdateBio()
    {
        _curbio = _resourceManager.biomass;
        _bioText.text = "Biomatter: " + _curbio;
    }
}
using TMPro;
using UnityEngine;

public class BioRateUIScript : MonoBehaviour
{
    private TextMeshProUGUI _rateText;
    private ResourceManager _resourceManager;

    // Start is called before the first frame update
    private void Start()
    {
        _rateText = GetComponent<TextMeshProUGUI>();
        _resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        float rate = _resourceManager.biomassRate;
        var sign = rate >= 0f ? "+" : "-";
        _rateText.text = sign + rate / VariableSetup.rate + "/" + "sec";
        _rateText.color = rate > 0f ? Color.green : Color.red;
    }
}
using TMPro;
using UnityEngine;

public class bioRateUIScript : MonoBehaviour
{
    private TextMeshProUGUI rateText;
    private ResourceManager resourceManager;

    // Start is called before the first frame update
    private void Start()
    {
        rateText = GetComponent<TextMeshProUGUI>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        var rate = resourceManager.biomassRate;
        var sign = rate >= 0f ? "+" : "-";
        rateText.text = sign + resourceManager.biomassRate + "/" + VariableSetup.rate + "sec";
        rateText.color = rate > 0f ? Color.green : Color.red;
        ;
    }
}
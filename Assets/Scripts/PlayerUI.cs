using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text heatText;
    public Text speedText;
    public Text fuelText;

    public string heatSuffix = "C";
    public string speedSuffix = " Km/h";
    public string fuelSuffix = " Fuel";

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateUI(TrainBehavior train)
    {
        if (train) {
            heatText.text = train.heat.ToString("F1") + heatSuffix;
            speedText.text = train.speed.ToString("F1") + speedSuffix;
            fuelText.text = train.fuel.ToString("F1") + fuelSuffix;
        } else {
            heatText.text = "20" + heatSuffix;
            speedText.text = "0" + speedSuffix;
            fuelText.text = "100" + fuelSuffix;
        }
    }
}

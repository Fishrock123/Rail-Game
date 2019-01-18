using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text heatText;
    public Text speedText;

    public string heatSuffix = "C";
    public string speedSuffix = " Km/h";

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateUI(TrainBehavior train)
    {
        if (train) {
            heatText.text = train.heat.ToString("F1") + heatSuffix;
            speedText.text = train.moveData.speed.ToString("F1") + speedSuffix;
        } else {
            heatText.text = "20" + heatSuffix;
            speedText.text = "0" + speedSuffix;
        }
    }
}

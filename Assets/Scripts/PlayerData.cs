using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public Color color;

    public string controlAccelerate = "Up";
    public string controlDecelerate = "Down";
    public string controlBurn = "Burn";
    public string controlVent = "Vent";

    public GameObject playerUIObject;
    public PlayerUI playerUI;

    public PlayerData (Color playerColor) {
        color = playerColor;

        GameObject obj = Resources.Load("Player UI") as GameObject;
        playerUIObject = Object.Instantiate(obj);

        playerUI = playerUIObject.GetComponent<PlayerUI>();
    }
}

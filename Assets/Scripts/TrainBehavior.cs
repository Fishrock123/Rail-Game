using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBehavior : MonoBehaviour
{
    public float accel = 0.2f;
    public float speed = 0f;
    public float maxSpeed = 20f;
    public float heat = 0f;
    public float maxHeat = 300f;
    public float heatGain = 10f;
    public float fuel = 100f;
    public float fuelCost = .5f;
    public float airResistance = .01f;

    public float ventHeatDuration = 3f;
    private float ventingHeat;

    public MoveData moveData = new MoveData();
    public RailSystem rail;

    public PlayerData playerData;

    public Color heatColor;
    public Color destroyedColor;

    public Renderer _renderer;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        moveData.lastMove = 0f;
        moveData.extra = 0f;
        moveData.currentTrack = rail.tracks.First.Value;
        moveData.train = this;

        material = _renderer.material;
        material.SetColor("_EmissionColor", playerData.color);
    }

    // Update is called once per frame
    void Update()
    {
        if (!rail) {
            return;
        }
        heat -= 2f * Time.deltaTime;
        if (speed > 0) {
            heat -= speed * airResistance * Mathf.Pow(speed, 2f) * Time.deltaTime;
            speed -= airResistance * Mathf.Pow(speed, 3f) * Time.deltaTime;
        }
        // Debug.Log(heat);
        // Debug.Log(speed);

        if (Input.GetButton(playerData.controlBurn) && fuel > 0f) {
            heat += heatGain * Time.deltaTime;
            fuel -= fuelCost * Time.deltaTime;
        }
        if (Input.GetButtonDown(playerData.controlVent) && heat > 250) {
            ventingHeat = ventHeatDuration;
        }
        if (ventingHeat > 0f) {
            ventingHeat -= Time.deltaTime;
            heat -= 50f * Time.deltaTime;
        }
        ventingHeat = Mathf.Clamp(ventingHeat, 0f, ventHeatDuration);

        fuel = Mathf.Clamp(fuel, 0, 1000f);
        heat = Mathf.Clamp(heat, 0, 1000f);
        float heatMod = (heat / maxHeat);
        // Magic curve that looks like what I want, 0-1
        // https://www.desmos.com/calculator/tgsp0ay1yc
        // float heatMod = (2.6f * Mathf.Sqrt(heatPercent)) + (-1.8f * heatPercent);
        if (heat > maxHeat) {
            heatMod -= (heat - maxHeat) / maxHeat;
            heat -= 10f * Time.deltaTime;
        }

        if (Input.GetButton(playerData.controlAccelerate)) {
            heat -= 3f * Time.deltaTime;
            speed += (accel * heatMod * Time.deltaTime);
        }
        if (Input.GetButton(playerData.controlDecelerate)) {
            if (speed > Mathf.Epsilon) {
                heat += heatGain * Time.deltaTime;
            }
            speed -= (accel * Time.deltaTime);
        }
        speed = Mathf.Clamp(speed, 0, maxSpeed);

        rail.UpdateTrain(transform, speed * Time.deltaTime, ref moveData);

        material.SetColor("_EmissionColor", Color.Lerp(playerData.color, heatColor, heatMod));

        playerData.playerUI.UpdateUI(this);
    }

    public void SetPlayer(PlayerData pData) {
        playerData = pData;
    }
}

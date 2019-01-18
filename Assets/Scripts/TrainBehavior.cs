using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBehavior : MonoBehaviour
{
    public float accel = 0.2f;
    public float _speed = 0f;
    public float maxSpeed = 1f;
    public float heat = 0f;
    public float maxHeat = 100f;
    public float heatGain = 10f;

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

        material = _renderer.material;
        material.SetColor("_EmissionColor", playerData.color);
    }

    // Update is called once per frame
    void Update()
    {
        if (!rail) {
            return;
        }
        heat -= 1f * Time.deltaTime;

        if (Input.GetButton("Jump")) {
            heat += heatGain * Time.deltaTime;
        }
        heat = Mathf.Clamp(heat, 0, maxHeat);
        float heatMod = heat / maxHeat;
        Debug.Log(heatMod);

        if (Input.GetButton(playerData.controlAccelerate)) {
            heat -= 2f * Time.deltaTime;
            moveData.speed += (accel * heatMod * Time.deltaTime);
        }
        if (Input.GetButton(playerData.controlDecelerate)) {
            if (moveData.speed > Mathf.Epsilon) {
                heat += heatGain * Time.deltaTime;
            }
            moveData.speed -= (accel * Time.deltaTime);
        }
        moveData.speed = Mathf.Clamp(moveData.speed, 0, maxSpeed);

        rail.UpdateTrain(transform, moveData.speed * Time.deltaTime, ref moveData);

        _speed = moveData.speed;

        material.SetColor("_EmissionColor", Color.Lerp(playerData.color, heatColor, heatMod));

        playerData.playerUI.UpdateUI(this);
    }

    public void SetPlayer(PlayerData pData) {
        playerData = pData;
    }
}

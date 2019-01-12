using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainBehavior : MonoBehaviour
{
    public float accel = 0.2f;
    public float _speed = 0f;
    public float maxSpeed = 1f;

    public MoveData moveData = new MoveData();
    public RailSystem rail;

    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        moveData.lastMove = 0f;
        moveData.extra = 0f;
        moveData.currentTrack = rail.tracks.First.Value;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rail) {
            return;
        }

        if (Input.GetButton(playerData.controlAccelerate)) {
            moveData.speed += (accel * Time.deltaTime);
        }
        if (Input.GetButton(playerData.controlDecelerate)) {
            moveData.speed -= (accel * Time.deltaTime);
        }
        moveData.speed = Mathf.Clamp(moveData.speed, -maxSpeed, maxSpeed);

        rail.UpdateTrain(transform, moveData.speed * Time.deltaTime, ref moveData);

        _speed = moveData.speed;
    }

    public void SetPlayer(PlayerData pData) {
        playerData = pData;
    }
}

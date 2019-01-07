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
        _speed = Mathf.Clamp(_speed + ((accel * Time.deltaTime) * Input.GetAxisRaw("Vertical")), -maxSpeed, maxSpeed);

        rail.UpdateTrain(transform, _speed * Time.deltaTime, ref moveData);
        Debug.Log(moveData.lastMove);
    }
}

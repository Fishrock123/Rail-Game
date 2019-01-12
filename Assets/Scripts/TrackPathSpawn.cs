using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPathSpawn : TrackPathBase
{
    public GameObject train;

    public override void ComputeValues()
    {
        _length = Mathf.Epsilon;
    }

    public override void UpdateLineRenderer() {}

    public override void MoveTransform(Transform t, float distance, ref MoveData moveData) {
        float moveDist = 0f;
        float total = moveData.lastMove + moveDist;
        moveData.extra = distance;
    }

    // Start is called before the first frame update
    void Start()
    {
        ComputeValues();
        UpdateLineRenderer();
    }

    public void Spawn (PlayerData playerData) {
        GameObject playerTrain = Object.Instantiate(train, transform.position,  Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + 90f));

        TrainBehavior behavior = playerTrain.GetComponent<TrainBehavior>();

        behavior.rail = rails;
        behavior.SetPlayer(playerData);
    }
}

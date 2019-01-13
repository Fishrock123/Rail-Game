using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public List<RailSystem> railSystems;
    public Material playerMaterial;

    // Start is called before the first frame update
    void Start()
    {
        foreach (RailSystem railSystem in railSystems) {
            TrackPathSpawn spawn = (TrackPathSpawn)railSystem.tracks.Last.Value;

            spawn.Spawn(new PlayerData(playerMaterial.GetColor("_EmissionColor")));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

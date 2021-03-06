﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSystem : MonoBehaviour
{
    public List<TrackPathBase> _tracks;
    public LinkedList<TrackPathBase> tracks;

    void Awake() {
        foreach (TrackPathBase tp in _tracks) {
            tp.rails = this;
        }

        tracks = new LinkedList<TrackPathBase>(_tracks);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTrain(Transform t, float distance, ref MoveData moveData) {
        TrackPathBase track = moveData.currentTrack;
        do {
            if (moveData.extra != 0f) {
                LinkedListNode<TrackPathBase> lln = tracks.Find(track);
                if (moveData.extra > 0f) {
                    if (lln.Next == null) {
                        lln = tracks.First;
                    } else {
                        lln = lln.Next;
                    }
                    moveData.lastMove = 0f;
                } else if (moveData.extra < 0f) {
                    if (lln.Previous == null) {
                        lln = tracks.Last;
                    } else {
                        lln = lln.Previous;
                    }
                    moveData.lastMove = 1f;
                }
                if (lln != null) {
                    track = lln.Value;
                }
                moveData.currentTrack = track;
                distance = moveData.extra;
                moveData.extra = 0f;
                if (lln == null) {
                    break;
                }
            }
            track.MoveTransform(t, distance, ref moveData);
        } while (moveData.extra > 0f);
    }
}

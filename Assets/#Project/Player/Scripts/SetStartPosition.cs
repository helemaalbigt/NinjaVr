using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SetStartPosition : MonoBehaviour {

    [SerializeField]
    private Realtime _realtime;

    [SerializeField]
    private Transform _player1StartingPoint;

    [SerializeField]
    private Transform _player2StartingPoint;

    void Start() {
        Transform startingPoint = _realtime.clientID == 0 ? _player1StartingPoint : _player2StartingPoint;

        transform.position = startingPoint.position;
        transform.rotation = startingPoint.rotation;
    }
}

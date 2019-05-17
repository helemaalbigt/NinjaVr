using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SetStartPosition : MonoBehaviour {

    [SerializeField]
    private Transform _playerHead;

    [SerializeField]
    private Transform _cameraRig;

    [SerializeField]
    private Realtime _realtime;

    [SerializeField]
    private Transform _player1StartingPoint;

    [SerializeField]
    private Transform _player2StartingPoint;

    void Start() {
        _realtime.didConnectToRoom += PlacePlayer;
    }

    void PlacePlayer(Realtime realtime) {
        Debug.Log("PlacePlayer ***");
        Transform startingPoint = _realtime.clientID == 0 ? _player1StartingPoint : _player2StartingPoint;

        Debug.Log("SetStartPosition PlacePlayer clientID " + _realtime.clientID + " " + startingPoint.name);


        _cameraRig.position = startingPoint.position;
        Vector3 temp = Vector3.ProjectOnPlane(startingPoint.position - _playerHead.position, Vector3.up);
        _cameraRig.Translate(temp);
        //_cameraRig.rotation = startingPoint.rotation;

        //_playerHead.position = new Vector3(startingPoint.position.x, _playerHead.position.y, startingPoint.position.z);
        //_playerHead.rotation = startingPoint.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ConnectToRoom : MonoBehaviour {

    public string roomName;

    [SerializeField]
    private Realtime _realtime;

    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
            _realtime.Connect(roomName);
    }
}

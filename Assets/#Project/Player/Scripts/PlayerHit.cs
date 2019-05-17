﻿using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class PlayerHit : MonoBehaviour {
    
    [SerializeField] private Transform _rightHand;
    [SerializeField] private Transform _leftHand;

    [SerializeField] private float _radius = 0.1f;
    [SerializeField] private LayerMask _headLayer;

    private NinjaGameManager _ninjaGameManager;

    void Start() {
        _ninjaGameManager = NinjaGameManager.currentInstance;
    }

    void Update() {
        if (GameUtils.instance.LocalPlayerAttacking()){
            CheckForHit(_rightHand);
            CheckForHit(_leftHand);
        }
    }

    void CheckForHit(Transform fistPoint) {
        Collider[] colliders = Physics.OverlapSphere(fistPoint.position, _radius, _headLayer);

        if (colliders.Length == 0)
            return;

        _ninjaGameManager.EndRoundEarly();
        Debug.Log(fistPoint.gameObject.name + " punched the head");

        var avatarView = colliders[0].GetComponentInParent<AvatarView>();
        if (avatarView != null)
        {
            avatarView.ShowBrokenHead();
            avatarView.AddForceToShards(300f, fistPoint.position);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_rightHand.position, _radius);
        Gizmos.DrawWireSphere(_leftHand.position,  _radius);
    }
}

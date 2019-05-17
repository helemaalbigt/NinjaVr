using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class HandControl : RealtimeComponent {

    public Animator _leftAnimator;
    public Animator _rightAnimator;

    private RealtimeView _realtimeView;

    private HandControlModel _model;
    public HandControlModel model {
        set { SetModel(value); }
        get { return _model; }
    }


    private void Start() {
        _realtimeView = GetComponent<RealtimeView>();
    }

    void Update() {
        if (!_realtimeView.isOwnedLocally)
            return;

        _model.leftHandAnimationValue  = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        _model.rightHandAnimationValue = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
    }

    void SetModel(HandControlModel model) {
        if (_model != null) {
            // Clear events
            _model.leftHandAnimationValueDidChange  -= LeftHandAnimationValueChanged;
            _model.rightHandAnimationValueDidChange -= RightHandAnimationValueChanged;

        }

        _model = model;

        if (_model != null) {
            // Register for events
            _model.leftHandAnimationValueDidChange  += LeftHandAnimationValueChanged;
            _model.rightHandAnimationValueDidChange += RightHandAnimationValueChanged;
        }
    }

    void LeftHandAnimationValueChanged(HandControlModel model, float value) {
        _leftAnimator.SetFloat("HandPos", value);
    }

    void RightHandAnimationValueChanged(HandControlModel model, float value) {
        _rightAnimator.SetFloat("HandPos", value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    public Animator _leftAnimator;
    public Animator _rightAnimator;

    void Update()
    {
        _leftAnimator.SetFloat("HandPos", OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger));
        _rightAnimator.SetFloat("HandPos", OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger));
    }
}

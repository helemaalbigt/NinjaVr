using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarView : MonoBehaviour
{
    public UserStyleList _style;
    public Renderer _head;
    public Renderer _handL;
    public Renderer _handR;

    private AttackingPlayer _player;
    public AttackingPlayer player
    {
        get { return _player; }
        set
        {
            SetStyle(_style.GetStyle(value));
            _player = value;
        }
    }

    void Start()
    {
        player = AttackingPlayer.one;
    }

    public void SetStyle(AttackerStyle style)
    {
        _head.material.color = style.avatarColor;
        _handL.material.color = style.avatarColor;
        _handR.material.color = style.avatarColor;
    }
}

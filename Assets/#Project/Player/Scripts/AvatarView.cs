using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class AvatarView : MonoBehaviour
{
    [SerializeField]
    private UserStyleList _userStyleList;

    public UserStyleList _style;
    public Renderer _head;
    public Renderer _handL;
    public Renderer _handR;
    public GameObject _eyes;

    private RealtimeView realtimeView;

    [SerializeField]
    private AttackingPlayer _player;
    public  AttackingPlayer  player
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
        realtimeView = GetComponent<RealtimeView>();

        int ownerID = realtimeView.ownerID;

        player = ownerID == 0 ? AttackingPlayer.one : AttackingPlayer.two;

        if (realtimeView.isOwnedLocally)
        {
            _eyes.SetActive(false);
        }
    }

    public void SetStyle(AttackerStyle style)
    {
        _head.material.color = style.avatarColor;
        _handL.material.color = style.avatarColor;
        _handR.material.color = style.avatarColor;
    }
}

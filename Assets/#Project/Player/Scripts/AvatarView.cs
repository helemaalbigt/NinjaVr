using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class AvatarView : MonoBehaviour
{
    public bool _isDummy;
    public UserStyleList _style;
    public Material _avatarReferenceMaterial;
    public Renderer _head;
    public Renderer _handL;
    public Renderer _handR;
    public GameObject _normalHeadWrapper;
    public GameObject _eyes;
    
    [Header("BrokenHead")]
    public GameObject _brokenWrapper;
    public Transform[] _shards;
    public Transform _leftEye;
    public Transform _rightEye;

    private RealtimeView realtimeView;
    private Material _avatarSharedMaterial;
    private Dictionary<int, Vector3> _shardLocalPositions = new Dictionary<int, Vector3>();
    public List<Rigidbody> _shardRigidBodies = new List<Rigidbody>();
    private Vector3 _leftEyeLocalPos;
    private Vector3 _rightEyeLocalPos;
    public Quaternion _shardsLocalRotation;

    [Space(15)]
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

    void Awake()
    {
        if (_isDummy)
        {
            SetupDummy();
        }
    }

    void Start()
    {
        realtimeView = GetComponent<RealtimeView>();
        _brokenWrapper.SetActive(false);

        _shardsLocalRotation = _shards[0].localRotation;
        GetBrokenLocalPositions();

        ShowRegularHead();

        if (!_isDummy)
        {
            int ownerID = realtimeView.ownerID;
            player = ownerID == 0 ? AttackingPlayer.one : AttackingPlayer.two;

            if (realtimeView.isOwnedLocally)
            {
                _eyes.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowBrokenHead();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowRegularHead();
        }
    }

    public void ShowBrokenHead()
    {
        SetAllRigidbodies(false);
        _brokenWrapper.transform.position = _normalHeadWrapper.transform.position;
        _brokenWrapper.transform.rotation = _normalHeadWrapper.transform.rotation;
        _brokenWrapper.SetActive(true);
        _normalHeadWrapper.SetActive(false);
    }

    public void ShowRegularHead()
    {
        _brokenWrapper.SetActive(false);
        ResetBrokenHead();
        _normalHeadWrapper.SetActive(true);
    }

    public void AddForceToShards(float force, Vector3 pos)
    {
        foreach (var bod in _shardRigidBodies)
        {
            bod.AddExplosionForce(force, pos, 0.5f);
        }
    }

    private void ResetBrokenHead()
    {
        SetAllRigidbodies(true);

        _leftEye.localPosition = _leftEyeLocalPos;
        _rightEye.localPosition = _rightEyeLocalPos;

        foreach (KeyValuePair<int, Vector3> shard in _shardLocalPositions)
        {
            _shards[shard.Key].localPosition = shard.Value;
            _shards[shard.Key].localRotation = _shardsLocalRotation;
        }
    }

    private void SetStyle(AttackerStyle style)
    {
        _avatarSharedMaterial = new Material(_avatarReferenceMaterial);
        _avatarSharedMaterial.color = style.avatarColor;

        _head.sharedMaterial = _avatarSharedMaterial;
        _handL.sharedMaterial = _avatarSharedMaterial;
        _handR.sharedMaterial = _avatarSharedMaterial;

        ColorShards();
    }

    private void ColorShards()
    {
        foreach (Transform child in _shards)
        {
            var rend = child.GetComponent<Renderer>();
            if (rend != null)
                rend.sharedMaterial = _avatarSharedMaterial;
        }
    }

    private void GetBrokenLocalPositions()
    {
        _leftEyeLocalPos = _leftEye.localPosition;
        _rightEyeLocalPos = _rightEye.localPosition;

        _shardRigidBodies.Add(_leftEye.GetComponent<Rigidbody>());
        _shardRigidBodies.Add(_rightEye.GetComponent<Rigidbody>());

        for (var i = 0; i < _shards.Length; i++) { 
            _shardLocalPositions.Add(i, _shards[i].localPosition);
            _shardRigidBodies.Add(_shards[i].GetComponent<Rigidbody>());
        }
    }

    private void SetAllRigidbodies(bool isKinematic)
    {
        foreach (var bod in _shardRigidBodies)
        {
            bod.useGravity = !isKinematic;
            bod.isKinematic = isKinematic;
        }
    }

    private void SetupDummy()
    {
        player = AttackingPlayer.none;

        var realtimeViews = GetComponentsInChildren<RealtimeView>();
        foreach (var rtv in realtimeViews)
        {
            rtv.enabled = false;
        }

        var realtimeTransforms = GetComponentsInChildren<RealtimeTransform>();
        foreach (var rtt in realtimeTransforms)
        {
            rtt.enabled = false;
        }

        GetComponent<RealtimeAvatar>().enabled = false;
        GetComponentInChildren<RealtimeAvatarVoice>().enabled = false;
    }
}

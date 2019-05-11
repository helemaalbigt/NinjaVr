using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ArenaView : MonoBehaviour
{
    public UserStyleList UserStyles;

    public Material _matBorder;
    public Light _spotLight;

    private Color _matDefaultColor;
    private Color _lightDefaultColor;

    void Awake()
    {
        _matDefaultColor = _matBorder.color;
        _lightDefaultColor = _spotLight.color;

        SetArenaView(UserStyles.GetStyle(AttackingPlayer.none));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetArenaView(UserStyles.GetStyle(AttackingPlayer.one));
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SetArenaView(UserStyles.GetStyle(AttackingPlayer.two));
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            SetArenaView(UserStyles.GetStyle(AttackingPlayer.none));
        }
    }

    public void SetArenaView(AttackerStyle style)
    {
        _matBorder.color = style.arenaColor;
        _spotLight.color = style.lightingColor;
    }
}

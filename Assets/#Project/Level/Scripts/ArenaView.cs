using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ArenaView : MonoBehaviour
{
    public NinjaGameManager _gameManager;

    [Space(25)]
    public UserStyleList UserStyles;
    public Material _matBorder;
    public Light _spotLight;

    private Color _matDefaultColor;
    private Color _lightDefaultColor;

    void Start()
    {
        _matDefaultColor = _matBorder.color;
        _lightDefaultColor = _spotLight.color;

        _gameManager.OnGameStateChanged += OnStateChanged;

        if(_gameManager.model != null)
            OnStateChanged((NinjaGameManager.GameState) _gameManager.model.gameState);
    }

    private void OnStateChanged(NinjaGameManager.GameState state)
    {
        switch (state)
        {
            case NinjaGameManager.GameState.P1AttackRound:
                SetArenaView(UserStyles.p1);
                break;

            case NinjaGameManager.GameState.P2AttackRound:
                SetArenaView(UserStyles.p2);
                break;

            default:
                SetArenaView(UserStyles.defaultStyle);
                break;
        }
    }

    public void SetArenaView(AttackerStyle style)
    {
        _matBorder.color = style.arenaColor;
        _spotLight.color = style.lightingColor;
    }
}

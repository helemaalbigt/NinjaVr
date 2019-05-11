using Normal.Realtime;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class ArenaView : MonoBehaviour
{
    public NinjaGameManager _gameManager;
    public Realtime _realtime;

    [Space(25)]
    public UserStyleList UserStyles;
    public Material _matBorder;
    public Light _spotLight;
    public Text[] _stateTexts;

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
        SetArenaViewFromState(state);
        SetStateBoards(state);
    }

    private int StateToId(NinjaGameManager.GameState state)
    {
        switch (state)
        {
            default:
            case NinjaGameManager.GameState.P1AttackRound:
               return 0;
                

            case NinjaGameManager.GameState.P2AttackRound:
                return 1;
        }
    }

    private void SetArenaViewFromState(NinjaGameManager.GameState state)
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

        if(_spotLight != null)
            _spotLight.color = style.lightingColor;
    }

    private void SetAllStateText(string text)
    {
        foreach (var stateText in _stateTexts)
        {
            stateText.text = text;
        }
    }

    private void SetStateBoards(NinjaGameManager.GameState state)
    {
        if (state == NinjaGameManager.GameState.P1AttackRound || state == NinjaGameManager.GameState.P2AttackRound)
        {
            int clientId = _realtime.clientID;
            if (clientId == StateToId(state))
            {
                SetAllStateText("Attack!");
            }
            else
            {
                SetAllStateText("Defend!");
            }
        }
        else
        {
            SetAllStateText("");
        }
    }
}

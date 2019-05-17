using Normal.Realtime;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class ArenaView : MonoBehaviour {
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

            case NinjaGameManager.GameState.GameResults:
                if (GameUtils.instance.IsIdWinner(0))
                {
                    SetArenaView(UserStyles.p1);
                }
                else
                {
                    SetArenaView(UserStyles.p2);
                }
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
        switch (state)
        {
            case NinjaGameManager.GameState.P1AttackRound:
            case NinjaGameManager.GameState.P2AttackRound:
                SetFightText(state);
                break;

            case NinjaGameManager.GameState.GameResults:
                SetWinner();
                break;

            case NinjaGameManager.GameState.Intro:
            default:
                SetAllStateText("Round Start");
                break;
        }
    }

    private void SetFightText(NinjaGameManager.GameState state)
    {
        int clientId = _realtime.clientID;
        if (clientId == GameUtils.instance.StateToClientId(state))
        {
            SetAllStateText("Attack!");
        }
        else
        {
            SetAllStateText("Defend!");
        }
    }

    private void SetWinner()
    {
        SetAllStateText(GameUtils.instance.IsIdWinner(0) ? 
            "Red Wins!" :
            "Blue Wins!");
    }
}

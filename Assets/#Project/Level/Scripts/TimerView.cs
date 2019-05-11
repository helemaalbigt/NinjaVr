using UnityEngine;

public class TimerView : MonoBehaviour
{
    public TextMesh _timertext;
    public NinjaGameManager _gameManager;

    private NinjaGameManager.GameState _state;

    void Start()
    {
        _gameManager.OnGameStateChanged += OnStateChanged;
    }

    private void OnStateChanged(NinjaGameManager.GameState state)
    {
        _state = state;
    }

    void Update()
    {
        if (IsInRound())
        {
            _timertext.text = StringUtils.FormatSeconds((float)(_gameManager.roundTimeLimit - _gameManager.roundElasped));
        }
        else
        {
            _timertext.text = "0:00";
        }
    }

    private bool IsInRound()
    {
        return _state == NinjaGameManager.GameState.Round1 || _state == NinjaGameManager.GameState.Round2;
    }
}

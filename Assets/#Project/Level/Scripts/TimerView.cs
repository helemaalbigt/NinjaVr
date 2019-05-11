using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    public Text _timertext;
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
            var time = (float) (_gameManager.roundTimeLimit - _gameManager.roundElasped);
            time = Mathf.Clamp(time, 0, float.PositiveInfinity);
            _timertext.text = StringUtils.FormatSeconds(time);
        }
        else
        {
            _timertext.text = "0:00";
        }
    }

    private bool IsInRound()
    {
        return _state == NinjaGameManager.GameState.P1AttackRound || _state == NinjaGameManager.GameState.P2AttackRound;
    }
}

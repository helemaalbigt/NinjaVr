using Normal.Realtime;
using UnityEngine;

public class GameUtils : MonoBehaviour
{
    public static GameUtils instance;

    public Realtime _realtime;
    public NinjaGameManager _gameManager;

    private void Awake()
    {
        instance = this;
    }

    public bool LocalPlayerAttacking()
    {
        return _realtime.clientID == StateToClientId(_gameManager.GetCurrentGameState());
    }

    public int StateToClientId(NinjaGameManager.GameState state)
    {
        switch (state)
        {
            case NinjaGameManager.GameState.P1AttackRound:
                return 0;


            case NinjaGameManager.GameState.P2AttackRound:
                return 1;

            default:
                return -1;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class NinjaGameManager : RealtimeComponent
{

    public event Action<GameState> OnGameStateChanged;

    [SerializeField]
    private double _roundElasped;
    public  double  roundElasped { get { return _roundElasped; } }

    [SerializeField]
    private double _roundTimeLimit = 5.0;
    public  double  roundTimeLimit { get { return _roundTimeLimit; } }

    private RealtimeView realtimeView;

    private Realtime realtime { get { return realtimeView.realtime; } }

    public static NinjaGameManager currentInstance;

    public enum GameState{
                Intro,
                SetUp,
               Round1,
        Round1Results,
               Round2,
        Round2Results,
          GameResults,
    }

    private NinjaGameManagerModel _model;
    public  NinjaGameManagerModel  model { set { SetModel(value); } }

    // TODO find a way to get the players owner ID of the player that is calling this?
    //private bool isMasterClient { get { return realtimeView.ownerID == 0; } }

    private void Awake() {
        currentInstance = this;

        realtimeView = GetComponent<RealtimeView>();
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            uint ninjaGameState = _model.gameState;
            ninjaGameState++;
            if (ninjaGameState > (uint)GameState.GameResults) {
               _model.gameState = 0;
            } else {
                _model.gameState = ninjaGameState;
            }
        }
    }

    void SetModel(NinjaGameManagerModel model) {
        if (_model != null) {
            // Clear events
            _model.gameStateDidChange -= GameStateChanged;
        }

        _model = model;

        if (_model != null) {
            // Register for events
            _model.gameStateDidChange += GameStateChanged;

            GameStateChanged(_model, _model.gameState);
        }
    }

    void GameStateChanged(NinjaGameManagerModel model, uint value) {
        Debug.Log("Current GameState: " + (GameState)value);

        OnGameStateChanged?.Invoke((GameState)value);

        switch (value) {
            case ((uint)NinjaGameManager.GameState.Intro):
                break;

            case ((uint)NinjaGameManager.GameState.SetUp):
                DoSetUp();
                break;

            case ((uint)NinjaGameManager.GameState.Round1):
                StartCoroutine(DoRound1());
                break;

            case ((uint)NinjaGameManager.GameState.Round1Results):
                    DoRound1Results();
                break;

            case ((uint)NinjaGameManager.GameState.Round2):
                StartCoroutine(DoRound2());
                break;

            case ((uint)NinjaGameManager.GameState.Round2Results):
                DoRound2Results();
                break;

            case ((uint)NinjaGameManager.GameState.GameResults):
                DoGameResults();
                break;
        }

    }

    void DoSetUp() {
        //if (isMasterClient)
        _model.gameState = (uint)GameState.Round1;
    }

    IEnumerator DoRound1() {
        //if (isMasterClient)
        _model.startTime = realtime.room.time;

        double elaspedTime;

        do {
            elaspedTime = realtime.room.time - _model.startTime;
            yield return null;
            _roundElasped  =   elaspedTime;

            if (_model.gameState != (uint)GameState.Round1)
                yield break;

        } while (elaspedTime < _roundTimeLimit);

        //if (isMasterClient)
        _model.gameState = (uint)GameState.Round1Results;
    }

    void DoRound1Results() {

    }

    IEnumerator DoRound2() {
       yield return null;
    }

    void DoRound2Results() {

    }

    void DoGameResults() {

    }
}

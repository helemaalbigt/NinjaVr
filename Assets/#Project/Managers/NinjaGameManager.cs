﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class NinjaGameManager : RealtimeComponent {

    public event Action<GameState> OnGameStateChanged;

    [SerializeField]
    private float _roundCount;
    public  float  roundCount { get { return _roundCount; } }

    [SerializeField]
    private float _roundElasped;
    public  float  roundElasped { get { return _roundElasped; } }

    [SerializeField]
    private double _roundTimeLimit = 5.0;
    public  double  roundTimeLimit { get { return _roundTimeLimit; } }

    private RealtimeView realtimeView;

    private Realtime realtime { get { return realtimeView.realtime; } }

    public static NinjaGameManager currentInstance;

    public enum GameState {
                Intro,
           RoundSetUp,
        P1AttackRound,
                Break,
        P2AttackRound,
          GameResults,
    }

    private NinjaGameManagerModel _model;

    public NinjaGameManagerModel model
    {
        set { SetModel(value); }
        get { return _model; }
    }

    private bool isMasterClient { get { return realtime.clientID == 0; } }

    private void Awake() {
        currentInstance = this;

        realtimeView = GetComponent<RealtimeView>();
    }
    
    private void Update() {
        if (!isMasterClient)
            return;

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

    public GameState GetCurrentGameState()
    {
        return (GameState) _model.gameState;
    }

    void SetModel(NinjaGameManagerModel model) {
        if (_model != null) {
            // Clear events
            _model.gameStateDidChange -= GameStateChanged;
            _model.winnerIdDidChange  -= WinnerIdChanged;
        }

        _model = model;

        if (_model != null) {
            // Register for events
            _model.gameStateDidChange += GameStateChanged;
            _model.winnerIdDidChange  += WinnerIdChanged;

            GameStateChanged(_model, _model.gameState);
        }
    }

    void GameStateChanged(NinjaGameManagerModel model, uint value) {
        Debug.Log("Current GameState: " + (GameState)value);

        OnGameStateChanged?.Invoke((GameState)value);

        switch (value) {
            case ((uint)NinjaGameManager.GameState.Intro):
                break;

            case ((uint)NinjaGameManager.GameState.RoundSetUp):
                DoRoundSetUp();
                break;

            case ((uint)NinjaGameManager.GameState.P1AttackRound):
                StartCoroutine(DoP1AttackRound());
                break;

            case ((uint)NinjaGameManager.GameState.Break):
                break;

            case ((uint)NinjaGameManager.GameState.P2AttackRound):
                StartCoroutine(DoP2AttackRound());
                break;

            case ((uint)NinjaGameManager.GameState.GameResults):
                DoGameResults();
                break;
        }

    }

    void WinnerIdChanged(NinjaGameManagerModel model, int value) {
        Debug.Log("WinnerIdChanged " + value);
    }

    void DoRoundSetUp() {

        if (isMasterClient)
            _model.gameState = (uint)GameState.P1AttackRound;
    }

    IEnumerator DoP1AttackRound() {
        if (isMasterClient)
            _model.startTime = (float)(realtime.room.time % 10000);

        yield return null;
        float elaspedTime;

        do {
            elaspedTime = (float)(realtime.room.time % 10000) - _model.startTime;
            yield return null;
            _roundElasped = elaspedTime;

            if (_model.gameState != (uint)GameState.P1AttackRound)
                yield break;

        } while (elaspedTime < _roundTimeLimit);

        if (isMasterClient)
            _model.gameState = (uint)GameState.P2AttackRound;
    }
        
    IEnumerator DoP2AttackRound() {
        if (isMasterClient)
            _model.startTime = (float)(realtime.room.time % 10000);
        
        yield return null;

        float elaspedTime;

        do {
            elaspedTime = (float)(realtime.room.time % 10000) - _model.startTime;
            yield return null;
            _roundElasped = elaspedTime;

            if (_model.gameState != (uint)GameState.P2AttackRound)
                yield break;

        } while (elaspedTime < _roundTimeLimit);

        _roundCount++;

        if (isMasterClient)
            _model.gameState = (uint)GameState.P1AttackRound;
    }

    void DoGameResults() {

    }

    public void EndRoundEarly(int winnerId)
    {
        if (!isMasterClient)
            return;

        _model.winnerId = winnerId;
        _model.gameState = (uint)GameState.GameResults;
    }
}

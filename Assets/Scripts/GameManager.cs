using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.SelectColor);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.SelectColor:
                HandleSelectColor();
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleSelectColor()
    {
        throw new NotImplementedException();
    }

    public enum GameState
    {
        SelectColor,
        PlayerTurn,
        EnemyTurn,
        Victory,
        Lose
    }
}

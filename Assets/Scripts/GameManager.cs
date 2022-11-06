using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
        SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
    }

    void Start()
    {

    }

    public void LoadScene()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN);
        SceneManager.LoadSceneAsync((int)SceneIndexes.DAY_1, LoadSceneMode.Additive);
    }

    public enum GameState
    {
        Menu,
        Open,
        Tutorial,
        Maeve,
        Final
    }

    public enum SceneIndexes
    {
        MANAGER = 0,
        TITLE_SCREEN = 1,
        DAY_1 = 2
    }
}

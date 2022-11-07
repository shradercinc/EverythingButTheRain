using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject loadingScreen;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
        int index = SceneManager.GetActiveScene().buildIndex;
        //Here, we check to see if we are in our manager. If not, we let the scene run normally
        if (index == (int)SceneIndexes.MANAGER)
        {
            SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
        }
    }

    void Start()
    {

    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadScene()
    {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.DAY_1, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for(int i = 0; i < scenesLoading.Count; i++)
        {
            while(!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        loadingScreen.gameObject.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject scenePlayer;
    public GameObject sceneTutorial;
    public GameObject sceneMaeve;
    public GameObject sceneFinal;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scenePlayer = GameObject.FindGameObjectWithTag("Player");
        sceneTutorial.GetComponent<Event_Tutorial>().StartCoroutine("TutorialSetup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //This playout should occur when the player steps into the trigger of the tutorial Event
    public void TutorialPlayout()
    {
        sceneTutorial.GetComponent<Event_Tutorial>().StartCoroutine("TutorialSetup");
        //Player enters trigger
        //Dialog
        //Wait for player to spin umbrella
        //Dialog
        //Destroy
    }

    void MaevePlayout()
    {

    }

    void FinalPlayout()
    {

    }

    void EndScene()
    {

    }
}

public enum GameState
{

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Tutorial : MonoBehaviour
{
    MeshRenderer myMesh;
    public GameObject _player;

    //UI for tutorial
    public GameObject tutorialUI;

    //Audio for player's reaction
    public AudioClip playerReactClip;

    // Start is called before the first frame update
    private void Awake()
    {
        myMesh = GetComponent<MeshRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine("TutorialSetup");
        }
    }

    IEnumerator TutorialSetup()
    {
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("TutorialAudio");
        //Open up the tutorialCanvas
        yield return new WaitForSeconds(1f);
        tutorialUI.SetActive(true);

        
    }

    //This object is controlling the tutorial event for the player

    //The player will be blocked from progressing down the street, due to a crowd in front of the dorm

    //The player will hear an voice clip play, and have UI appear which shows the player how to use their umbrella
}

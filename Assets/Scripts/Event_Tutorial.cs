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

    //bool tracks if players is in the right spot to splash
    bool inTrig = false;
    //bool to track if we already played tutorial
    bool hasPlayed = false;


    //Tracks the Rigidbody of player's umbrella
    Rigidbody umbRigid;

    // Start is called before the first frame update
    private void Awake()
    {
        myMesh = GetComponent<MeshRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        umbRigid = _player.transform.Find("Umbrella_Temp").GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrig)
        {
            //If the player spins like they were supposed to

            if (Mathf.Abs(umbRigid.angularVelocity.y) > 3)
           {
                Destroy(gameObject);
           }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (!hasPlayed)
            {
                //gm.TutorialPlayout();
                StartCoroutine("TutorialSetup");
            }
        }

        inTrig = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrig = false;
    }

    IEnumerator TutorialSetup()
    {
        Debug.Log("Is playing tutorial");
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("TutorialAudio");
        //Open up the tutorialCanvas
        tutorialUI.SetActive(true);
    }

    //This object is controlling the tutorial event for the player

    //The player will be blocked from progressing down the street, due to a crowd in front of the dorm

    //The player will hear an voice clip play, and have UI appear which shows the player how to use their umbrella
}

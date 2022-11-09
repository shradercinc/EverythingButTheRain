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

    //This is a prototype way to have states on this event. Any numbers not listed is a error
    // 1 = Tutorial
    // 2 = Maeve
    // 3 = Final
    public int STATE_STATIC = -1;

    public EventStatus STATUS;



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
        STATUS = EventStatus.START;
    }

    // Update is called once per frame
    void Update()
    {

        if(STATUS == EventStatus.START)
        {
            Debug.Log("This is still true");
        }
        if (inTrig)
        {
            //If the player spins like they were supposed to

            if (Mathf.Abs(umbRigid.angularVelocity.y) > 3)
           {
                if (STATE_STATIC == 1)
                {
                    _player.GetComponent<PlayerDialog>().StartCoroutine("TutorialAftermathAudio");
                    tutorialUI.SetActive(false);
                    Destroy(gameObject);
                }

                if(STATE_STATIC == 2)
                {
                    _player.GetComponent<PlayerDialog>().StartCoroutine("MaeveAftermathAudio");
                    Destroy(gameObject);
                }
           }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!hasPlayed)
            {
                //Tutorial
                if(STATE_STATIC == 1)
                {
                    //gm.TutorialPlayout();
                    StartCoroutine("TutorialSetup"); //Make a enum variable that equals the variable string
                }
                else if(STATE_STATIC == 2)
                {
                    StartCoroutine("MaeveSetup");
                }
                else if(STATE_STATIC == 3)
                {
                    StartCoroutine("FinalSetup");
                }
                else
                {
                    Debug.Log("Invalid Static State");
                }
            }
        }

        inTrig = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inTrig = false;
    }

    //Have a general event state
    //Then afterwards, do your checks for the specific events
    //Are they doing the same thing? Then call the same code


    IEnumerator TutorialSetup()
    {
        Debug.Log("Is playing tutorial");
        hasPlayed = true;
        myMesh.enabled = false;
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("TutorialAudio");
        //Open up the tutorialCanvas
        tutorialUI.SetActive(true);
    }

    IEnumerator MaeveSetup()
    {
        hasPlayed = true;
        myMesh.enabled = false;
        Debug.Log("We will cue Maeve");
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("MaeveIntroAudio");
    }


    IEnumerator FinalSetup()
    {
        hasPlayed = true;
        myMesh.enabled = false;
        Debug.Log("We will cue Final");
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("FinalIntroAudio");
    }

}

public enum EventStatus
{
    START,
    TUTORIAL,
    MAEVE,
    FINAL,
    END
}

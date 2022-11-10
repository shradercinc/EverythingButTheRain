using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event_Tutorial : MonoBehaviour
{

    Scene currentScene;
    [SerializeField] private FadeToBlack f2b;

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
    // 4 = End (Doors to NYU)
    
    //Day 2
    // 5 = Group 1
    // 6 = Group 2
    // 7 = Group 3
    // 8 = 

    public int STATE_STATIC = -1;

    public EventStatus STATUS;



    //Tracks the Rigidbody of player's umbrella
    Rigidbody umbRigid;

    // Start is called before the first frame update
    private void Awake()
    {
        currentScene = gameObject.scene; //This is how we check if we are in day 1 or 2
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
            if (currentScene.name == "Day2") //If we are in Day 2
            {
                StartCoroutine(StartDayTwoSetup());
                STATUS = EventStatus.VOID;
            }
            else
            {
                StartCoroutine(StartSetup());
                STATUS = EventStatus.VOID;
            }
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
                    GameObject.FindGameObjectWithTag("NPC").GetComponent<NPCFollow>().isFollowing = true;
                    _player.GetComponent<PlayerDialog>().StartCoroutine("MaeveAftermathAudio");
                    Destroy(gameObject);
                }
                if(STATE_STATIC == 3)
                {
                    _player.GetComponent<PlayerDialog>().StartCoroutine("FinalAftermathAudio");
                    Destroy(gameObject);
                }
                if (STATE_STATIC == 5) //Day 2 Group1
                {
                    _player.GetComponent<PlayerDialog>().StartCoroutine("TwoGroupOneAftermathAudio");
                    Destroy(gameObject);
                }
                if (STATE_STATIC == 6) //Day 2 Group2
                {
                    _player.GetComponent<PlayerDialog>().StartCoroutine("TwoGroupTwoAftermathAudio");
                    Destroy(gameObject);
                }
                if (STATE_STATIC == 7) //Day 2 Group3
                {
                    _player.GetComponent<PlayerDialog>().StartCoroutine("TwoGroupThreeAftermathAudio");
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
                    STATUS = EventStatus.TUTORIAL;
                }
                else if(STATE_STATIC == 2)
                {
                    StartCoroutine("MaeveSetup");
                    STATUS = EventStatus.MAEVE;
                }
                else if(STATE_STATIC == 3)
                {
                    StartCoroutine("FinalSetup");
                    STATUS = EventStatus.FINAL;
                }
                else if(STATE_STATIC == 4) //End of the day in Day 1
                {
                    StartCoroutine(DayTwoGroupOneSetup());
                    STATUS = EventStatus.DAYTWO_EVENT;
                }
                else if (STATE_STATIC == 5) //Day 2 First Group
                {
                    StartCoroutine(GroupOneDayTwoSetup());
                    STATUS = EventStatus.DAYTWO_EVENT;
                }
                else if (STATE_STATIC == 6) //Day2 Second Group
                {
                    StartCoroutine(GroupTwoDayTwoSetup());
                    STATUS = EventStatus.DAYTWO_EVENT;
                }
                else if (STATE_STATIC == 7) //Day2 Third Group
                {
                    StartCoroutine(GroupThreeDayTwoSetup());
                    STATUS = EventStatus.DAYTWO_EVENT;
                }
                else if (STATE_STATIC == 8) //End dialog (Before you enter classroom)
                {
                    StartCoroutine(EndingDialogDayTwoSetup());
                    STATUS = EventStatus.DAYTWO_EVENT;
                }
                else if(STATE_STATIC == 9)//End of scene (walk through) door
                {
                    StartCoroutine(EndGame());
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

    IEnumerator StartSetup()
    {
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("StartGameAudio");
    }

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

    IEnumerator EndSetup()
    {
        //We need to fill this with code to switch scenes (Day2 and weather transition)
        yield return null;
    }

    IEnumerator StartDayTwoSetup()
    {
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("StartTwoAudio");
    }

    //We should rename this as it will get confusing
    IEnumerator DayTwoGroupOneSetup()
    {
        f2b.Fade();
        yield return null;
    }

    //I want to put this into DayTwoGroupOneSetup(), but I'm worried about messing up Ryan's fade code
    IEnumerator GroupOneDayTwoSetup()
    {
        hasPlayed = true;
        myMesh.enabled = false;
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("TwoGroupOneIntroAudio");
    }

    IEnumerator GroupTwoDayTwoSetup()
    {
        hasPlayed = true;
        myMesh.enabled = false;
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("TwoGroupTwoIntroAudio");
    }

    IEnumerator GroupThreeDayTwoSetup()
    {
        hasPlayed = true;
        myMesh.enabled = false;
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("TwoGroupThreeIntroAudio");
    }

    IEnumerator EndingDialogDayTwoSetup()
    {
        hasPlayed = true;
        myMesh.enabled = false;
        yield return _player.GetComponent<PlayerDialog>().StartCoroutine("TwoEndReactAudio");
        Destroy(gameObject);
    }

    IEnumerator EndGame()
    {
        f2b.Fade(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return null;
    }


}

public enum EventStatus
{
    START,
    TUTORIAL,
    MAEVE,
    FINAL,
    END,
    DAYTWO_EVENT, //This is how we track if we are in a event or not
    VOID //This is a empty event for right now. Nothing should happen when event is set to this
}

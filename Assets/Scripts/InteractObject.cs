 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractObject : MonoBehaviour
{

    [SerializeField] Transform player;

    //Handling the Title of the gameobject
    [SerializeField] string objectTitle = "GameObject";
    [SerializeField] GameObject text;
    [SerializeField] TextMeshPro textComponent;

    //This bool is how we keep track if the player is looking at the object
    private bool isLooking = false;

    //The audio clip which will be the main characters reaction to this object
    [SerializeField] AudioClip reactAudio;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //We set up the text object, and turn it off
        text = gameObject.transform.GetChild(1).gameObject; //The index of the Title Gameobject is 1 in children
        textComponent = text.GetComponent<TextMeshPro>();
        textComponent.text = objectTitle;
        text.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This makes the NPC always stare at the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        if(Input.GetKeyDown(KeyCode.F) && isLooking) 
        {
            //Debug.Log("WE WILL PLAY THE AUDIO");
            PlayerReaction();
        }
    }

    private void OnMouseEnter()
    {
        //Debug.Log("Turn on Text");
        text.SetActive(true);
        isLooking = true;
    }

    private void OnMouseExit()
    {
        text.SetActive(false);
        isLooking = false;
    }


    public void PlayerReaction()
    {
        AudioSource source = player.gameObject.GetComponent<AudioSource>();
        if(!source.isPlaying)
        {
            source.PlayOneShot(reactAudio);
        }
    }
}

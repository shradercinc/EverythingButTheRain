using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RainColide : MonoBehaviour
{
    //EventState dictates at what point in the game the player is, and what audio will play at the respective time
    //using this system dialogue has to be completed in a semi-linear fashion.
    //All audio clips will be attached to their relative cronological event.
    // 1 = Tutorial audio
    // 2 = Meave Intro 
    // 3 = End of day 1
    private float EventState = 0;
    public bool talking = false;
    private AudioSource aud;
    public AudioClip[] clips;

    private Transform pos;


    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        aud = GetComponent<AudioSource>();

        //this is simply because we don't have something that sets eventstate to 1, or the tutorial dialogue
        EventState = 1;
    }


    private void OnTriggerEnter(Collider other)
    {

        
        if (other.gameObject.CompareTag("Crowd Agent"))
        {
            //print("hit");
            Debug.Log("Hit crowd");
            var agent = other.gameObject.GetComponent<CrowdControl>();
            var agentPos = agent.transform.position;
            var rainCollidePos = transform.position;
            var fleeDir = agentPos - new Vector3(rainCollidePos.x, agentPos.y, rainCollidePos.z);
            agent.Flee(fleeDir);
            Debug.DrawRay(agent.transform.position, fleeDir, Color.red, 2f);
            agent.isWandering = true;
        }

        if(other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Hit NPC");
            other.gameObject.GetComponent<NPCFollow>().isFollowing = true;

        //Starts Meave intro dialogue if the tutorial is complete 
            if (EventState == 1)
            {
                EventState = 2;
                aud.PlayOneShot(clips[1]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aud.isPlaying)
        {
            talking = true;
        }
        else 
        {
            talking = false;
        }
    }
}

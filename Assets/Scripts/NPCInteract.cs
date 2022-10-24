using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    float state = 0;
    public bool happy = false;
    public float stateSet = 0;
    public AudioClip[] clips;
    private AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        print($"state:{state}");
        print($"set:{stateSet}");
        if (state == 0)
        {
            state = stateSet;
        }
        //play unhappy splash
        if (state == 1)
        {
            state = 1.1f;
            aud.PlayOneShot(clips[0]);
            print("Splashed");
        }
        //play interact
        if (state == 2)
        {
            state = 2.1f;
            aud.PlayOneShot(clips[1]);
        }
        //play happy splash
        if (state == 3)
        {
            state = 3.1f;
            aud.PlayOneShot(clips[2]);
        }
        if (state != 0 && aud.isPlaying == false) 
        {
            state = 0;
        }
        stateSet = 0;
    }
}

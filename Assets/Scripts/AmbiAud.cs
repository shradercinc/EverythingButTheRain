using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiAud : MonoBehaviour
{
    private AudioSource aud;
    private float Max = 0.8f;
    private float Vol = 0.8f;
    public GameObject Talker;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        Max = aud.volume;
        Vol = Max;
    }

    // Update is called once per frame
    void Update()
    {
        if (Talker.GetComponent<PlayerDialog>().talking == false)
        {
            Vol = Max;
        }
        else
        {
            Vol = Max / 2;
        }

        if (aud.volume <= Vol)
        {
            aud.volume += 0.05f;
        }
        else
        {
            aud.volume -= 0.05f;
        }
    }
}

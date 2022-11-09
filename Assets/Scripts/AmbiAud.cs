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
    }
}

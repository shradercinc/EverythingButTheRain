using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmAmbiController : MonoBehaviour
{
    private AudioSource aud;
    public float Max = 0.8f;
    public float Vol = 0;
    public GameObject RC;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        Max = aud.volume;
        Vol = Max;
    }

    // Update is called once per frame
    void Update()
    {
        if (RC.GetComponent<RainColide>().talking == false)
        {
            Vol = Max;
            if (aud.volume < Vol)
            {
                aud.volume += 0.05f;
            }
            print("no talk");
        }
        else
        {
            Vol = Max * 0.5f;
            if (aud.volume > Vol)
            {
                aud.volume -= 0.1f;
            }
            print("talk");
        }
    }
}

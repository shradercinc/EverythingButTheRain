using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAud : MonoBehaviour
{
    AudioSource aud;
    public float cDif = 0.1f;
    private float Max = 0.8f;
    private float Vol = 0.8f;
    public GameObject Talker;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        Max = aud.volume;
        Vol = Max;
        aud.volume = 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Crowd Agent"))
        {
            aud.volume += cDif;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Crowd Agent"))
        {
            aud.volume -= cDif;
        }
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

        if (aud.volume >= Vol)
        {
            aud.volume -= 0.05f;
        }
        if (aud.volume <= 0.1f)
        {
            aud.volume = 0.1f;
        }
    }
}

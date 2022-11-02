using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAud : MonoBehaviour
{
    AudioSource aud;
    public float cDif = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
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
        if (aud.volume <= 0.2f)
        {
            aud.volume = 0.2f;
        }
    }
}

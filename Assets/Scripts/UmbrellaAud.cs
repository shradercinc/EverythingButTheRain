using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaAud : MonoBehaviour
{
    private AudioSource aud;
    public float spinAudR = 4;
    public float vMod = 0.1f;
    public GameObject Pl;
    [SerializeField] Transform playerUmbrella = null;


    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.volume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        var rotV = playerUmbrella.GetComponent<Rigidbody>().angularVelocity;
        if (rotV == Vector3.Slerp(rotV, Vector3.zero, .1f))
        {
            aud.volume -= vMod;
            if (aud.volume <= 0)
            {
                aud.volume = 0;
            }
        }
        else
        {
            aud.volume += vMod;
            if (aud.volume >= 1)
            {
                aud.volume = 1;
            }
        }

        aud.pitch = rotV.magnitude / spinAudR;
        if (aud.pitch <= 1)
        {
            aud.pitch = 1;
        }
        aud.outputAudioMixerGroup.audioMixer.SetFloat("pitchBend", 1f / aud.pitch);

    }
}

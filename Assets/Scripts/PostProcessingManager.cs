using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingManager : MonoBehaviour
{
    private Volume _transitionVolume;
    
    [SerializeField] private ParticleSystem rain;
    [SerializeField] private ParticleSystem fog;
    [SerializeField] private ParticleSystem umbrella;

    [SerializeField] private float transitionTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _transitionVolume = GetComponent<Volume>();
        ResetDay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            BrightenDay();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            ResetDay();
        }
    }

    public void BrightenDay()
    {
        StartCoroutine(ToTheEndOfRain());
    }

    public void ResetDay()
    {
        _transitionVolume.weight = 0;
        rain.Play();
        fog.Play();
        umbrella.Play();
    }

    private IEnumerator ToTheEndOfRain()
    {
        rain.Stop();
        while(_transitionVolume.weight < 1)
        {
            _transitionVolume.weight += Time.deltaTime / transitionTime;
            if(_transitionVolume.weight > 0.5f) fog.Stop();
            yield return null;
        }
        umbrella.Stop();
        _transitionVolume.weight = 1;
    }
}

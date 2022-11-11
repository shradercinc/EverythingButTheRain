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
    [SerializeField] private MeshRenderer umbrellaCanvas;
 
    [SerializeField] private float transitionTime;

    [SerializeField] private float _transparency;
    
    // Start is called before the first frame update
    void Start()
    {
        _transitionVolume = GetComponent<Volume>();
        _transparency = umbrellaCanvas.material.GetFloat("_Transparency");
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
            umbrellaCanvas.material.SetFloat("_Transparency", (1 - _transitionVolume.weight) * _transparency);
            if (_transitionVolume.weight > 0.5f)
            {
                fog.Stop();
            }
            if (_transitionVolume.weight > 0.7f)
            {
                umbrella.Stop();
            }
            yield return null;
        }
        umbrellaCanvas.material.SetFloat("_Transparency", 0);
        _transitionVolume.weight = 1;
    }
}

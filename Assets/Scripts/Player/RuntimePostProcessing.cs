using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class RuntimePostProcessing : MonoBehaviour
{
    private Volume _volume;
    private Vignette _vignette;
    
    // Start is called before the first frame update
    void Start()
    {
        _volume = FindObjectOfType<Volume>();
        _volume.profile.TryGet(out _vignette);
    }

    // Update is called once per frame
    void Update()
    {
        _vignette.intensity.value += Time.deltaTime * 0.1f;
    }
}

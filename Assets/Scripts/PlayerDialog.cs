using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the script responsible for playing Evan's Dialog
public class PlayerDialog : MonoBehaviour
{

    [SerializeField]AudioSource talkSource;

    //Clips for tutorial
    [SerializeField]AudioClip tutorialReact;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    IEnumerator TutorialAudio()
    {
        talkSource.clip = tutorialReact;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the script responsible for playing Evan's Dialog
public class PlayerDialog : MonoBehaviour
{
    [Header("General")]
    [SerializeField]AudioSource talkSource;
    public bool talking = false;

    //Clips for start
    [Header("Day 1 Audio")]
    [SerializeField] AudioClip startDialog;

    //Clips for tutorial
    [SerializeField]AudioClip tutorialReact;
    [SerializeField] AudioClip tutorialAftermath;

    //Clips for Maeve
    [SerializeField] AudioClip maeveReact;
    [SerializeField] AudioClip maeveAftermath;

    //Clips for final
    [SerializeField] AudioClip finalReact;
    [SerializeField] AudioClip finalAftermath;

    [Header("Day 2 Audio")]
    [SerializeField] AudioClip twoStartDialog;


    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (talkSource.isPlaying == true)
        {
            talking = true;
        }
        else
        {
            talking = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    IEnumerator StartGameAudio()
    {
        talkSource.clip = startDialog;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator TutorialAudio()
    {
        talkSource.clip = tutorialReact;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator TutorialAftermathAudio()
    {
        Debug.Log("Playing Aftermath audio");
        talkSource.clip = tutorialAftermath;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator MaeveIntroAudio()
    {
        Debug.Log("Playing Maeve React audio");
        talkSource.clip = maeveReact;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator MaeveAftermathAudio()
    {
        talkSource.clip = maeveAftermath;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator FinalIntroAudio()
    {
        Debug.Log("Playing Final Audio");
        talkSource.clip = finalReact;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }
    
    IEnumerator FinalAftermathAudio()
    {
        talkSource.clip = finalAftermath;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying); 
    }

}

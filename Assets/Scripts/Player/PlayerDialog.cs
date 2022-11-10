using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the script responsible for playing Evan's Dialog
public class PlayerDialog : MonoBehaviour
{
    [Header("General")]
    [SerializeField]AudioSource talkSource;
    public bool talking = false;
    [SerializeField]PostProcessingManager skyManage;

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

    //Clips for group one
    [SerializeField] AudioClip groupOneReact;
    [SerializeField] AudioClip groupOneAftermath;

    //Clips for group two
    [SerializeField] AudioClip groupTwoReact;
    [SerializeField] AudioClip groupTwoAftermath;

    //Clips for group three
    [SerializeField] AudioClip groupThreeReact;
    [SerializeField] AudioClip groupThreeAftermath;
    [SerializeField] AudioClip stopRaining;

    //Clip for ending
    [SerializeField] AudioClip dayTwoEndReact;


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

    //DAY 1 COROUTINES!!!

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

    //DAY 2 COROUTINES!!!

    IEnumerator StartTwoAudio()
    {
        Debug.Log("Playing Start 2");
        talkSource.clip = twoStartDialog;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator TwoGroupOneIntroAudio()
    {
        Debug.Log("Playing Group 1 React");
        talkSource.clip = groupOneReact;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator TwoGroupOneAftermathAudio()
    {
        Debug.Log("Playing Group 1 Aftermath");
        talkSource.clip = groupOneAftermath;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator TwoGroupTwoIntroAudio()
    {
        Debug.Log("Playing Group 2 React");
        talkSource.clip = groupTwoReact;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator TwoGroupTwoAftermathAudio()
    {
        Debug.Log("Playing Group 2 Aftermath");
        talkSource.clip = groupTwoAftermath;
        talkSource.Play();
        skyManage.BrightenDay();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator TwoGroupThreeIntroAudio()
    {
        Debug.Log("Playing Group 3 React");
        talkSource.clip = groupThreeReact;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator TwoGroupThreeAftermathAudio()
    {
        Debug.Log("Playing Group 3 Aftermath");
        talkSource.clip = groupThreeAftermath;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

    IEnumerator TwoEndReactAudio()
    {
        Debug.Log("Playing EndingDialog");
        talkSource.clip = dayTwoEndReact;
        talkSource.Play();
        yield return new WaitWhile(() => talkSource.isPlaying);
    }

}

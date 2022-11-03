using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the script responsible for playing Evan's Dialog
public class PlayerDialog : MonoBehaviour
{

    [SerializeField]AudioSource talkSource;

    //Clips for tutorial
    [SerializeField]AudioClip tutorialReact;
    [SerializeField] AudioClip tutorialAftermath;

    //Clips for Maeve
    [SerializeField] AudioClip maeveReact;
    [SerializeField] AudioClip maeveAftermath;


    private void Awake()
    {
        talkSource = GetComponent<AudioSource>();
    }

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

    IEnumerator FinalIntroAudio()
    {
        Debug.Log("Playing Final Audio");
        yield return new WaitForSeconds(1f);
    }

}

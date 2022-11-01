using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Tutorial : MonoBehaviour
{
    MeshRenderer myMesh;

    // Start is called before the first frame update
    private void Awake()
    {
        myMesh = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            myMesh.enabled = false;
            StartCoroutine("TutorialSetup");
        }
    }

    IEnumerator TutorialSetup()
    {
        Debug.Log("WAIT!");
        yield return new WaitForSeconds(2f);
        Debug.Log("DONE");
    }

    //This object is controlling the tutorial event for the player

    //The player will be blocked from progressing down the street, due to a crowd in front of the dorm

    //The player will hear an voice clip play, and have UI appear which shows the player how to use their umbrella
}

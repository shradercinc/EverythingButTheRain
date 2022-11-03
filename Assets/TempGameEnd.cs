using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameEnd : MonoBehaviour
{

    public GameObject tempEndingScreen;

    private void Awake()
    {
        tempEndingScreen.SetActive(false);
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
        if(other.tag == "Player")
        {
            StartCoroutine("EndPlaytest");
        }
    }

    IEnumerator EndPlaytest()
    {
        tempEndingScreen.SetActive(true);
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}

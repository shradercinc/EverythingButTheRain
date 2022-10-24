using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainColide : MonoBehaviour
{
    private Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
    }


    private void OnTriggerEnter(Collider other)
    {
        print("Colide");
        if (other.gameObject.GetComponent<NPCInteract>())
        {
            if (other.gameObject.GetComponent<NPCInteract>().happy == false)
            {
                other.gameObject.GetComponent<NPCInteract>().stateSet = 1;
            }
            else
            {
                other.gameObject.GetComponent<NPCInteract>().stateSet = 3;
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

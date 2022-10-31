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

        if (other.gameObject.CompareTag("Crowd Agent"))
        {
            Debug.Log("Hit crowd");
            var agent = other.gameObject.GetComponent<CrowdControl>();
            agent.Flee(agent.transform.position - transform.position);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

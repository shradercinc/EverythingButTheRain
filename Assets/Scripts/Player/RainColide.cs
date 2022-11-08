using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        //print("hitany");
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
            //print("hit");
            Debug.Log("Hit crowd");
            var agent = other.gameObject.GetComponent<CrowdControl>();
            var agentPos = agent.transform.position;
            var rainCollidePos = transform.position;
            var fleeDir = agentPos - new Vector3(rainCollidePos.x, agentPos.y, rainCollidePos.z);
            agent.isWandering = true;
            agent.Flee(fleeDir);
            Debug.DrawRay(agent.transform.position, fleeDir, Color.red, 2f);
          
        }

        if (other.gameObject.CompareTag("Crowd Box"))
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Hit NPC");
            other.gameObject.GetComponent<NPCFollow>().isFollowing = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

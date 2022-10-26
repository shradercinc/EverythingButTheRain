using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
    [SerializeField] NavMeshAgent npc;
    [SerializeField] Transform player;

    private void Awake()
    {
        npc = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This makes the NPC always stare at the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        npc.SetDestination(player.position);
    }
}

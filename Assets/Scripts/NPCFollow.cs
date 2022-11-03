using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
    [SerializeField] NavMeshAgent npc;
    [SerializeField] Transform player;

    //This bool will freeze the NPC until the player splashes them!
    public bool isFollowing;

    private void Awake()
    {
        npc = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isFollowing = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowing)
        {
            //This makes the NPC always stare at the player
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            npc.SetDestination(player.position);
        }
    }
}

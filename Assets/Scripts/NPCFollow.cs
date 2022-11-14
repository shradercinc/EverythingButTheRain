using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
    [SerializeField] NavMeshAgent npc;
    [SerializeField] Transform player;

    private bool isLooking = false;
    private AudioSource aud;
    public AudioClip[] Clip;


    //This bool will freeze the NPC until the player splashes them!
    public bool isFollowing;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        npc = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        isFollowing = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Reaction()
    {
        aud.PlayOneShot(Clip[Random.Range(0, Clip.Length)]);
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
        if (Input.GetKeyDown(KeyCode.F) && isLooking && !player.GetComponent<PlayerDialog>().talking && !aud.isPlaying)
        {
            //Debug.Log("WE WILL PLAY THE AUDIO");
            Reaction();
        }
        if (aud.isPlaying)
        {
            player.GetComponent<PlayerDialog>().talking = true;
        }
        if (!aud.isPlaying && !player.GetComponent<PlayerDialog>().talking)
        {
            player.GetComponent<PlayerDialog>().talking = false;
        }
    }
    private void OnMouseEnter()
    {
        isLooking = true;
    }
    private void OnMouseExit()
    {
        isLooking = false;
    }
}

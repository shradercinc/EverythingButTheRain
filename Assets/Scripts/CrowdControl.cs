using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class CrowdControl : MonoBehaviour
{
    private int _currGoal;
    private NavMeshAgent _agent;
    private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(CrowdManager.Instance.goals[0].transform.position);
        _agent.speed *= Random.Range(0.5f, 2f);
    }

    private void NextGoal()
    {
        _currGoal = Random.Range(0, 5);
        _agent.SetDestination(CrowdManager.Instance.goals[_currGoal].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance <= 0.5f)
        {
            NextGoal();
        }
    }
}

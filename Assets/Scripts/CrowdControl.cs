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
    private float _spd;
    private float _angularSpd;
    [SerializeField] private float fleeRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(CrowdManager.Instance.goals[0].transform.position);
        _agent.speed *= Random.Range(0.5f, 2f);
        _spd = _agent.speed;
        _angularSpd = _agent.angularSpeed;
    }

    private void NextGoal()
    {
        _currGoal = Random.Range(0, 5);
        _agent.SetDestination(CrowdManager.Instance.goals[_currGoal].transform.position);
    }

    private void ResetAgent()
    {
        _agent.speed = _spd;
        _agent.angularSpeed = _angularSpd;
        _agent.ResetPath();
    }

    public void Flee(Vector3 dir)
    {
        Vector3 newGoal = transform.position + dir.normalized * fleeRadius;
        var path = new NavMeshPath();
        _agent.CalculatePath(newGoal, path);
        if (path.status != NavMeshPathStatus.PathInvalid)
        {
            _agent.SetDestination(path.corners[^1]);
            _agent.speed = 10;
            _agent.angularSpeed = 500;
        }
        else
        {
            _agent.speed = 10;
            _agent.angularSpeed = 300;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.remainingDistance <= 0.5f)
        {
            ResetAgent();
            NextGoal();
        }
    }
}

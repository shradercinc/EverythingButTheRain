using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdManager : MonoBehaviour
{
    private static CrowdManager _instance;

    public static CrowdManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public Transform player;
    public CrowdGoal[] goals;

    void Awake()
    {
        _instance = this;
        player = FindObjectOfType<PlayerController>().transform;
        SortByDistanceFromPlayer();
    }

    private float _wait = 30;
    private void FixedUpdate()
    {
        if (_wait > 0) _wait -= Time.deltaTime;
        else
        {
            _wait = 30;
            SortByDistanceFromPlayer();
        }
    }

    public void SortByDistanceFromPlayer()
    {
        GoalComparer comparer = new GoalComparer();
        Array.Sort(goals, comparer);
    }

    private class GoalComparer : IComparer<CrowdGoal>
    {
        public int Compare(CrowdGoal x, CrowdGoal y)
        {
            var position = CrowdManager.Instance.player.position;
            return (int) (Vector3.Distance(position, x.transform.position) - Vector3.Distance(position, y.transform.position));
        }
    }
}

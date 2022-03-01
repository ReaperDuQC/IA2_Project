using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Agent
{
    [SerializeField]
    Transform _destination;

    private void Start()
    {
        _agent.SetDestination(_destination.position);
    }
}

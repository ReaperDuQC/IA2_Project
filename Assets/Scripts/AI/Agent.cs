using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour
{
    protected NavMeshAgent _agent;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
}

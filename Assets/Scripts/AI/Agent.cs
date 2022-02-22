using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    [SerializeField] Transform startingPosition;
    [SerializeField] Transform endingPosition;
    NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        agent.transform.position = startingPosition.position;
        agent.SetDestination(endingPosition.position);
    }
}

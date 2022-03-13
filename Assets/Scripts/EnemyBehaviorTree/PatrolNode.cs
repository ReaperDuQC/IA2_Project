using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PatrolNode : Node
{
    NavMeshAgent agent;
    Vector3[] patrolPositions;
    public PatrolNode(NavMeshAgent agent, Vector3[] patrolPositions)
    {
        this.agent = agent;
        this.patrolPositions = patrolPositions;
    }
    public override NodeStates Evaluate()
    {
        int randomIndex = Random.Range(0, patrolPositions.Length);
        agent.SetDestination(patrolPositions[randomIndex]);
        return NodeStates.SUCCESS;
    }
}

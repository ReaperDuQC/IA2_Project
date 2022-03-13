using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsIdleNode : Node
{
    NavMeshAgent agent;

    public IsIdleNode(NavMeshAgent agent)
    {
        this.agent = agent;
    }

    public override NodeStates Evaluate()
    {
        bool isIdle = !agent.pathPending && agent.remainingDistance < agent.stoppingDistance;
        return isIdle ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}

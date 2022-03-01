using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ChaseNode : Node
{
    NavMeshAgent agent;
    Transform target;
    public ChaseNode(NavMeshAgent agent, Transform target)
    {
        this.agent = agent;
        this.target = target;
    }
    public override NodeStates Evaluate()
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
        return NodeStates.SUCCESS;
    }
}

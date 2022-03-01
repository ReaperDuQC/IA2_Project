using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SleepNode : Node
{
    NavMeshAgent agent;
    public SleepNode(NavMeshAgent agent)
    {
        this.agent = agent;
    }
    public override NodeStates Evaluate()
    {
        agent.isStopped = true;
        return NodeStates.SUCCESS;
    }
}

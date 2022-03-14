using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseSelector : Selector
{
    NavMeshAgent agent;
    float maxStoppingDistance;
    public ChaseSelector(List<Node> nodes, NavMeshAgent agent, float maxStoppingDistance) : base(nodes)
    {
        this.agent = agent;
        this.maxStoppingDistance = maxStoppingDistance;
    }

    public override NodeStates Evaluate()
    {
        var resultState = base.Evaluate();
        if (resultState != NodeStates.RUNNING)
        {
            bool isSuccessful = resultState == NodeStates.SUCCESS;
            agent.stoppingDistance = isSuccessful ? 1.0f : maxStoppingDistance;
        }
        return resultState;
    }
}

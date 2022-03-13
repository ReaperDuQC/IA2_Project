using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseSelector : Selector
{
    NavMeshAgent agent;
    BT_Enemy enemyBT;
    float maxStoppingDistance;
    public ChaseSelector(List<Node> nodes, NavMeshAgent agent, BT_Enemy enemyBT, float maxStoppingDistance) : base(nodes)
    {
        this.agent = agent;
        this.enemyBT = enemyBT;
        this.maxStoppingDistance = maxStoppingDistance;
    }

    public override NodeStates Evaluate()
    {
        var resultState = base.Evaluate();
        if (resultState != NodeStates.RUNNING)
        {
            bool isSuccessful = resultState == NodeStates.SUCCESS;
            agent.stoppingDistance = isSuccessful ? 1.0f : maxStoppingDistance;
            enemyBT.IsChasing = isSuccessful;
        }
        return resultState;
    }
}

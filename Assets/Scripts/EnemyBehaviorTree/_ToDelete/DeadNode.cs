using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeadNode : Node
{
    NavMeshAgent agent;
    public DeadNode(NavMeshAgent agent)
    {
        this.agent = agent;
    }
    public override NodeStates Evaluate()
    {
        if (agent.enabled == true)
        { 
            agent.enabled = false;
        }
        m_nodeState = NodeStates.SUCCESS;
        return m_nodeState;
    }
}

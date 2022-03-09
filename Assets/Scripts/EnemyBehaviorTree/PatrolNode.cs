using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PatrolNode : Node
{
    NavMeshAgent agent;
    Vector3 boundryBottomLeft;
    float width;
    float depth;
    public PatrolNode(NavMeshAgent agent, Collider ground)
    {
        this.agent = agent;
        boundryBottomLeft = ground.bounds.min;
        width = ground.bounds.size.x;
        depth = ground.bounds.size.z;
    }
    public override NodeStates Evaluate()
    {
        Vector3 randomDestination = boundryBottomLeft + new Vector3(Random.Range(0, width), 0, Random.Range(0, depth));
        agent.SetDestination(randomDestination);
        return NodeStates.SUCCESS;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargetInRange : Node
{
    Transform origin;
    Transform target;
    float distance;
    public IsTargetInRange(Transform origin,Transform target, float distance)
    {
        this.origin = origin;
        this.target = target;
        this.distance = distance;
    }
    public override NodeStates Evaluate()
    {
        float currentDistance = Vector3.Distance(origin.position, target.position);
        m_nodeState = currentDistance < distance ? NodeStates.SUCCESS : NodeStates.FAILURE;
        return m_nodeState;
    }
}

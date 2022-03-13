using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTargetInSightRangeNode : Node
{
    Transform origin;
    Transform target;
    float distance;
    public IsTargetInSightRangeNode(Transform origin, Transform target, float distance)
    {
        this.origin = origin;
        this.target = target;
        this.distance = distance;
    }
    public override NodeStates Evaluate()
    {
        Physics.Raycast(origin.position, target.position - origin.position, out RaycastHit hit, distance);
        bool isInSightRange = hit.collider?.transform == target;
        DebugDrawRay(isInSightRange); //
        m_nodeState = isInSightRange ? NodeStates.SUCCESS : NodeStates.FAILURE;
        return m_nodeState;
    }
    void DebugDrawRay(bool isInSightRange)
    {
        if (Vector3.Distance(target.position, origin.position) < 2.0f * distance)
        {
            Color rayColor = isInSightRange ? Color.green : Color.red;
            Debug.DrawRay(origin.position, target.position - origin.position, rayColor);
        }
    }
}

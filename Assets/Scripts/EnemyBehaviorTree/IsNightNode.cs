using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNightNode : Node
{
    DayNightSystem dayNight;
    public IsNightNode(DayNightSystem dayNight)
    {
        this.dayNight = dayNight;
    }
    public override NodeStates Evaluate()
    {
        m_nodeState = dayNight.IsNight() ? NodeStates.SUCCESS : NodeStates.FAILURE;
        return m_nodeState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttackReadyNode : Node
{
    bool isReady;
    float lastAttackTime;
    float timeBetweenAttack;

    public IsAttackReadyNode(float timeBetweenAttack) 
    {
        this.timeBetweenAttack = timeBetweenAttack; 
        isReady = true;
        lastAttackTime = 0f;
    }
    public override NodeStates Evaluate()
    {
        isReady = Time.time - lastAttackTime >= timeBetweenAttack;
        m_nodeState = isReady ? NodeStates.SUCCESS : NodeStates.FAILURE;

        if(m_nodeState == NodeStates.SUCCESS)
        {
            lastAttackTime = Time.time;
        }
        return m_nodeState;
    }
}

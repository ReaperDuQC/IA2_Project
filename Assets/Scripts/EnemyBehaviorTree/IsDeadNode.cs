using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDeadNode : Node
{
    //CompleteProject.EnemyHealth health;
    public IsDeadNode(/*CompleteProject.EnemyHealth health*/)
    {
       // this.health = health;
    }
    public override NodeStates Evaluate()
    {
       //m_nodeState = health.currentHealth <= 0 ? NodeStates.SUCCESS : NodeStates.FAILURE;
        return m_nodeState;
    }
}

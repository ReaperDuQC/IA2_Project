using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node
{
    public AttackNode() 
    {

    }
    public override NodeStates Evaluate()
    {
        // TODO: trigger lose condition
        return NodeStates.SUCCESS;
    }
}

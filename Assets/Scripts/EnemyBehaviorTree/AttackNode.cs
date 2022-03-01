using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node
{
    //CompleteProject.PlayerHealth health;
    int damage;
    public AttackNode(/*CompleteProject.PlayerHealth health,*/ int damage) 
    {
        //this.health = health;
        this.damage = damage;
    }
    public override NodeStates Evaluate()
    {
        //health.TakeDamage(damage);
        return NodeStates.SUCCESS;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAnyEnemyInRangeChasingNode : Node
{
    Transform enemy;
    Transform player;
    float distanceCrowding;
    float distanceChase;
    int layerMask;

    public IsAnyEnemyInRangeChasingNode(Transform enemy, Transform player, float distanceCrowding, float distanceChase, int layerMask)
    {
        this.enemy = enemy;
        this.player = player;
        this.distanceCrowding = distanceCrowding;
        this.distanceChase = distanceChase;
        this.layerMask = layerMask;
    }

    public override NodeStates Evaluate()
    {
        Ray ray = new Ray(enemy.position, Vector3.up);
        RaycastHit[] hits = Physics.SphereCastAll(ray, distanceCrowding, 0, layerMask);

        bool isCrowdChasing = false;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.GetComponent<EnemyBehaviorTree>() && (player.position - hit.collider.transform.position).sqrMagnitude < distanceChase * distanceChase)
            {
                Debug.DrawRay(enemy.position, hit.transform.position - enemy.position);
                isCrowdChasing |= true;
            }
        }

        return isCrowdChasing ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}

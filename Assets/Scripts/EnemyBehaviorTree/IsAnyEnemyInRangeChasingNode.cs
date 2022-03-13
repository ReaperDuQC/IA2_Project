using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAnyEnemyInRangeChasingNode : Node
{
    BT_Enemy enemyBT;
    Transform enemy;
    Transform player;
    float distanceCrowding;
    float distanceChase;
    int enemyLayerMask;

    public IsAnyEnemyInRangeChasingNode(Transform enemy, Transform player, float distanceCrowding, float distanceChase, int enemyLayerMask)
    {
        this.enemyBT = enemyBT;
        this.enemy = enemy;
        this.player = player;
        this.distanceCrowding = distanceCrowding;
        this.distanceChase = distanceChase;
        this.enemyLayerMask = enemyLayerMask;
    }

    public override NodeStates Evaluate()
    {
        Ray ray = new Ray(enemy.position, Vector3.up);
        RaycastHit[] hits = Physics.SphereCastAll(ray, distanceCrowding, 0, enemyLayerMask);

        bool isCrowdChasing = false;
        foreach (RaycastHit hit in hits)
        {
            Physics.Raycast(hit.collider.transform.position, player.position - hit.collider.transform.position, out RaycastHit hit2, distanceChase);
            bool isInSightRange = hit2.collider?.transform == player;
            if (isInSightRange)
            {
                Debug.DrawRay(enemy.position, hit.transform.position - enemy.position);
                isCrowdChasing |= true;
            }
        }

        return isCrowdChasing ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}

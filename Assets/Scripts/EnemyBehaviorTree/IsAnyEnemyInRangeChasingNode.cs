using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAnyEnemyInRangeChasingNode : Node
{
    Transform enemy;
    Transform player;
    float distanceCrowding;
    float distanceChase;
    float soundTimerCrowding = 0;
    float soundCooldownCrowding = 5.0f;
    int enemyLayerMask;
    bool isDebugMode = true;

    public IsAnyEnemyInRangeChasingNode(Transform enemy, Transform player, float distanceCrowding, float distanceChase, int enemyLayerMask)
    {
        this.enemy = enemy;
        this.player = player;
        this.distanceCrowding = distanceCrowding;
        this.distanceChase = distanceChase;
        this.enemyLayerMask = enemyLayerMask;
    }

    public override NodeStates Evaluate()
    {
        // get nearby other enemies
        Ray ray = new Ray(enemy.position, Vector3.up);
        RaycastHit[] hits = Physics.SphereCastAll(ray, distanceCrowding, 0, enemyLayerMask);

        bool isCrowdChasing = false;
        foreach (RaycastHit hit in hits)
        {
            // check if any other enemy in range is chasing the player
            Physics.Raycast(hit.collider.transform.position, player.position - hit.collider.transform.position, out RaycastHit hit2, distanceChase);
            bool isInSightRange = hit2.collider?.transform == player;
            if (isInSightRange)
            {
                isCrowdChasing = true;
                if (isDebugMode)
                {
                    Debug.DrawRay(enemy.position, hit.transform.position - enemy.position);
                }
                else
                {
                    break;
                }
            }
        }

        if (isCrowdChasing && Time.time > soundCooldownCrowding + soundTimerCrowding)
        {
            soundTimerCrowding = Time.time;
            AudioSource source = enemy.GetComponent<AudioSource>();
            if (source != null && source.clip != null)
            {
                source.Play();
            }
            //play sound
        }

        return isCrowdChasing ? NodeStates.SUCCESS : NodeStates.FAILURE;
    }
}
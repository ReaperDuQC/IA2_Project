using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class BT_Enemy : MonoBehaviour
{
    [SerializeField] float m_distanceToAttack;
    [SerializeField] float m_distanceToChase;
    [SerializeField] float m_distanceCrowding;
    [SerializeField] float m_maxStoppingDistance;
    [SerializeField] LayerMask m_enemyLayerMask;

    public bool IsChasing { get; set; }

    Selector m_rootNode;
    Sequence m_attackNode;
    Sequence m_chaseNode;
    Sequence m_patrolNode;

    private void Update()
    {
        m_rootNode.Evaluate();
    }

    public void InitializeEnemy(Transform player, Vector3[] patrolPositions)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        // Attack Sequence
        IsTargetInSightRangeNode isPlayerInAttackRange = new IsTargetInSightRangeNode(transform, player, m_distanceToAttack);
        AttackNode attackNode = new AttackNode();
        List<Node> attackChildren = new List<Node>();
        attackChildren.Add(isPlayerInAttackRange);
        attackChildren.Add(attackNode);
        m_attackNode = new Sequence(attackChildren);

        // Chase Sequence
        IsTargetInSightRangeNode isPlayerInChaseRange = new IsTargetInSightRangeNode(transform, player, m_distanceToChase);
        IsAnyEnemyInRangeChasingNode isCrowding = new IsAnyEnemyInRangeChasingNode(transform, player, m_distanceCrowding, m_distanceToChase, m_enemyLayerMask.value);
        List<Node> chaseChildren = new List<Node>();
        List<Node> chaseSeletorChildren = new List<Node>();
        chaseSeletorChildren.Add(isPlayerInChaseRange);
        chaseSeletorChildren.Add(isCrowding);
        Selector chaseSelector = new ChaseSelector(chaseSeletorChildren, agent, this, m_maxStoppingDistance);
        ChaseNode chaseNode = new ChaseNode(agent, player);
        chaseChildren.Add(chaseSelector);
        chaseChildren.Add(chaseNode);
        m_chaseNode = new Sequence(chaseChildren);

        // Patrol Sequence
        PatrolNode patrolNode = new PatrolNode(agent, patrolPositions);
        IsIdleNode isIdleNode = new IsIdleNode(agent);
        List<Node> patrolChildren = new List<Node>();
        patrolChildren.Add(isIdleNode);
        patrolChildren.Add(patrolNode);
        m_patrolNode = new Sequence(patrolChildren);

        // Root Selector
        List<Node> rootChildren = new List<Node>();
        rootChildren.Add(m_attackNode);
        rootChildren.Add(m_chaseNode);
        rootChildren.Add(m_patrolNode);
        m_rootNode = new Selector(rootChildren);
    }
}

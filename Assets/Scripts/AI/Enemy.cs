using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Agent
{
    [SerializeField]
    GameController _gameController;
    Vector3 desination;
    private void Start()
    {
        desination = _gameController.GetEndingPos();
       _agent.SetDestination(desination);
    }


    private void Update()
    {
        Vector3 dir = desination - transform.position;
        Debug.DrawRay(transform.position, dir, Color.red);
    }
}

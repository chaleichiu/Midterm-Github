using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyIdleState : EnemyState
{
    int currentTarget = 0;
    //constructor 
    public EnemyIdleState(EnemyController enemy) : base(enemy) { }


    public override void OnStateEnter()
    {
        enemy.agent.destination = enemy.targetPoints[currentTarget].position;
        Debug.Log("Enemy is Idle Enter");
    }
    public override void OnStateExit()
    {
        Debug.Log("Enemy Exiting idling");
    } 
    public override void OnStateUpdate()
    {
        if (enemy.agent.remainingDistance < 0.2f)
        {
            currentTarget++;
            if (currentTarget >= enemy.targetPoints.Length)
                currentTarget = 0;
            enemy.agent.destination = enemy.targetPoints[currentTarget].position;
        }

        //check for player
        if (Physics.SphereCast(enemy.enemyEye.position, enemy.playerCheckRadius, enemy.transform.forward, out RaycastHit hit, enemy.playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player Found!!");

                enemy.player = hit.transform;
                enemy.agent.destination = enemy.player.position;

                //Move to follow state
                enemy.ChangeState(new EnemyFollowState(enemy));
            }
        }
    }

}

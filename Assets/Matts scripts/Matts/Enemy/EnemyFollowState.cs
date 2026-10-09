using UnityEngine;

public class EnemyFollowState : EnemyState
{
    float distanceToPlayer;

    //constructor
    public EnemyFollowState(EnemyController enemy) : base(enemy) { }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy will be following the player");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy will not follow the player");
    }

    public override void OnStateUpdate()
    {
        if (enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

            if (distanceToPlayer > 10)
            {
                enemy.ChangeState(new EnemyIdleState(enemy));
            }

            if (distanceToPlayer < 2)
            {
                enemy.ChangeState(new EnemyAttackState(enemy));
            }
            enemy.agent.destination = enemy.player.position;
        }
        else
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }
}

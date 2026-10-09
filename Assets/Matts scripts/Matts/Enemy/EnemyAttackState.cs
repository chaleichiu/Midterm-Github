using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float distanceToPlayer;
    Health playerHealth;
    float damgeOverTime = 20f;

    public EnemyAttackState(EnemyController enemy) : base(enemy) 
    {
        playerHealth = enemy.player.GetComponent<Health>();
    }

    void Attack()
    {
        if(playerHealth != null)
        {
            playerHealth.DeductHealth(damgeOverTime * Time.deltaTime);
        }
    }

    public override void OnStateEnter()
    {
        Debug.Log("Enemy is attacking Player");
    }

    public override void OnStateExit()
    {
        Debug.Log("Enemy is NOT attacking player");
    }

    public override void OnStateUpdate()
    {
        Attack();

        if (enemy.player != null)
        {
            distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);

            if (distanceToPlayer > 2)
            {
                enemy.ChangeState(new EnemyFollowState(enemy));
            }
            enemy.agent.destination = enemy.player.position;
        }
        else
        {
            enemy.ChangeState(new EnemyIdleState(enemy));
        }
    }
}

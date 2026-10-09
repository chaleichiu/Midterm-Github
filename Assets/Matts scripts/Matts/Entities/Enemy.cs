using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Patrol around 3 points, Enemy linear vison from the front, and if the player enters
/// it will begin to follow them.
/// </summary>

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] targetPoints;
    [SerializeField] private Transform enemyEye;
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private float playerCheckRadius = 0.4f;

    int currentTarget = 0;

    private NavMeshAgent agent;

    public bool isIdle = true;
    public bool isPlayerFound;
    public bool isCloseToPlayer;

    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = targetPoints[currentTarget].position;       
    }

    private void Update()
    {
        if (isIdle)
        {
            Idle();
        }
        else if (isPlayerFound)
        {
            if (isCloseToPlayer)
            {
                AttackPlayer();
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    void Idle()
    {
        if (agent.remainingDistance < 0.2f)
        {
            currentTarget++;
            if(currentTarget >= targetPoints.Length)
                currentTarget= 0;
            agent.destination = targetPoints[currentTarget].position;
        }

        //check for player
        if (Physics.SphereCast(enemyEye.position, playerCheckRadius, transform.forward, out RaycastHit hit, playerCheckDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Player Found!!");
                isIdle = false;
                isPlayerFound= true;
                player = hit.transform;
                agent.destination = player.position;

            }
        }
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) > 10)
            {
                isPlayerFound = false;
                isIdle = true;
            }

            // Attack
            if (Vector3.Distance(transform.position, player.position) < 2)
            {
                isCloseToPlayer = true;
            }
            else
            {
                isCloseToPlayer = false;
            }

            agent.destination = player.position;
        }
        else
        {
            isPlayerFound= false;
            isIdle = true;
            isCloseToPlayer = false;
        }
    }
    void AttackPlayer()
    {
        Debug.Log("Attacking player!!! AHHHHHH!");
        if (Vector3.Distance(transform.position, player.position) > 2)
        {
            isCloseToPlayer= false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyEye.position, playerCheckRadius);
        Gizmos.DrawWireSphere(enemyEye.position + enemyEye.forward * playerCheckDistance, playerCheckRadius);
        Gizmos.DrawLine(enemyEye.position, enemyEye.position + enemyEye.forward * playerCheckDistance);
    }
}

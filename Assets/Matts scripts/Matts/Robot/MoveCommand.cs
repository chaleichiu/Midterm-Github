using UnityEngine;
using UnityEngine.AI;


public class MoveCommand : Command
{
    private NavMeshAgent agent;
    private Vector3 destination;

    public MoveCommand(NavMeshAgent agent, Vector3 destination)
    {
        this.agent = agent;
        this.destination = destination;
    }

    public override void Execute()
    {
        agent.SetDestination(destination);
    }

    /*
    public override bool isComplete
    {
        get { return ReachedDestination(); }
    }
    */
    
    /// <summary>
    /// This bool variable, get its return value from the reacheddestination method.
    /// </summary>
    public override bool isComplete => ReachedDestination();

    bool ReachedDestination()
    {
        if (agent.remainingDistance > 0.2f)
        {
            return false;
        }
        return true;
    }
}

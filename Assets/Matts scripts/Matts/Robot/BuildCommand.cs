using UnityEngine;
using UnityEngine.AI;

public class BuildCommand : Command
{
    private NavMeshAgent agent;
    private Builder builder;

    public BuildCommand(NavMeshAgent agent, Builder builder)
    {
        this.agent = agent;
        this.builder = builder;
    }

    public override void Execute()
    {
        agent.SetDestination(builder.transform.position);
    }

    public override bool isComplete => BuildComplete();

    bool BuildComplete()
    {
        if (agent.remainingDistance > 0.1f)
            return false;

        if (builder != null)
            builder.Build();
        return true;
    }

}

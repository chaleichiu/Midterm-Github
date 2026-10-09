using UnityEngine;

public abstract class Command
{
    public abstract bool isComplete { get; }
    public abstract void Execute();
}

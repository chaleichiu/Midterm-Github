using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private Input inputType;

    [Header("Gun")]
    public MeshRenderer gunRender;
    public Color bulletGunColor;
    public Color rocketGunColor;

    [Header("ObjectPools")]
    public ObjectPool bulletPool;
    public ObjectPool rocketPool;

    private IShootStrategy currentShootStrategy;

    [SerializeField] private float shootVelocity;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private PlayerMovementBehaviour playerMovementBehaviour;

    private float finalShootVelocity;

    public override void Interact()
    {
        // Set a default shoot Strategy
        if (currentShootStrategy == null)
        {
            currentShootStrategy = new BulletShootStrategy(this);
        }
    
        if (input.weapon1Pressed)
        {
            currentShootStrategy = new BulletShootStrategy(this);
        }
        if (input.weapon2Pressed)
        {
            currentShootStrategy = new RocketShootStrategy(this);
        }

        // Shoot with the selected strategy
        if (input.primaryShootPressed && currentShootStrategy != null)
        {
            currentShootStrategy.Shoot();
        }
        if (input.secondaryShootPressed && currentShootStrategy != null)
        {
            currentShootStrategy.AlternativeShoot();
        }
    }
    public float GetShootVelocity()
    {
        finalShootVelocity = playerMovementBehaviour.GetForwardSpeed() + shootVelocity;
        return finalShootVelocity;
    }

    public Transform GetShootPoint()
    {
        return shootPoint;
    }
}

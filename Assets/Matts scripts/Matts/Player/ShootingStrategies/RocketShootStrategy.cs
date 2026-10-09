using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{
    ShootInteractor _interactor;
    Transform shootPoint;
    float rocketVelocity = 6f;

    public RocketShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to Rocket mode");
        _interactor = interactor;
        shootPoint = interactor.GetShootPoint();

        // change gun color
        interactor.gunRender.material.color = interactor.rocketGunColor;
    }

    public void Shoot()
    {
        PooledObject pooledRocket = _interactor.rocketPool.GetPooledObject();
        pooledRocket.gameObject.SetActive(true);


        Rigidbody rocket = pooledRocket.GetComponent<Rigidbody>();
        rocket.transform.position = shootPoint.position;
        rocket.transform.rotation = shootPoint.rotation;
        rocket.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        rocket.linearVelocity = shootPoint.forward * _interactor.GetShootVelocity() / rocketVelocity;

        _interactor.rocketPool.DestroyPooledObject(pooledRocket, 5f);
    }
    public void AlternativeShoot()
    {
        Debug.Log("None unlocked yet!");
    }
}

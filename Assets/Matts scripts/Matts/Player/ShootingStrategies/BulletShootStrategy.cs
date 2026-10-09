using UnityEngine;

public class BulletShootStrategy : IShootStrategy
{
    ShootInteractor _interactor;
    Transform shootPoint;

    public BulletShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switched to bullet mode");
        _interactor = interactor;
        shootPoint = interactor.GetShootPoint();

        // change gun color
        interactor.gunRender.material.color = interactor.bulletGunColor;
    }

    public void Shoot()
    {
        PooledObject pooledBullet = _interactor.bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);


        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.linearVelocity = shootPoint.forward * _interactor.GetShootVelocity();

        _interactor.bulletPool.DestroyPooledObject(pooledBullet, 5f);
    }

    public void AlternativeShoot()
    {
        Debug.Log("None unlocked yet!");
    }
}

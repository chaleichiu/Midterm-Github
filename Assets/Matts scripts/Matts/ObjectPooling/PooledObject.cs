using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnReset;

    ObjectPool associatedPool;

    private float timer;
    private bool setToDestory = false;
    private float destroyTime = 0f;

    public void SetObjectPool(ObjectPool pool)
    {
        associatedPool = pool;
        timer = 0;
        destroyTime = 0f;
        setToDestory = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (setToDestory)
        {
            timer += Time.deltaTime;

            if (timer >= destroyTime)
            {
                setToDestory = false;
                timer = 0;
                setToDestory = false;
                Destroy();
            }
        }
    }

    public void Destroy()
    {
        if (associatedPool != null)
        {
            associatedPool.RestoreObject(this);
        }
    }

    public void Destory(float time)
    {
        setToDestory = true;
        destroyTime = time;
    }

    public void ResetObject()
    {
        OnReset?.Invoke();
    }
}

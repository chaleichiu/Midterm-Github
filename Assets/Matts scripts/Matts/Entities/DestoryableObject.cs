using UnityEngine;

public class DestoryableObject : MonoBehaviour, IDestroyable
{

    public void OnCollided()
    {
        Destroy(gameObject);
    }
}

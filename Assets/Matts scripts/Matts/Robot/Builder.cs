using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject objectToBuild;
    [SerializeField] private Transform placementPoint;

    public void Build()
    {
        Instantiate(objectToBuild, placementPoint.position, placementPoint.rotation);

        // destory build space
        Destroy(gameObject);
    }
}

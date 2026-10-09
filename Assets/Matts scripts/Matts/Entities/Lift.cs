using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private bool isUp;
    [SerializeField] private float speed;

    bool isMoving;
    Vector3 targetPosition;

    public void ToggleLift()
    {
        if (isMoving)
            return;

        if (isUp)
        {
            //move back down
            targetPosition = transform.localPosition - new Vector3(0, moveDistance, 0);
            isUp = false;
        }
        else
        {
            targetPosition = transform.localPosition + new Vector3(0, moveDistance, 0);
            isUp = true;
        }
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.localPosition, targetPosition) < 0.05f)
        {
            isMoving = false;
        }
    }
}

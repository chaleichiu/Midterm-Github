using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    [SerializeField] private UITimer uiTimer;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool triggerOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && triggerOnce) return;

        if (other.CompareTag(playerTag))
        {
            uiTimer.StartTimer();
            hasTriggered = true;
        }
    }
}
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private KeycardManager keycardManager;
    [SerializeField] private GameObject UiShowCardMissing;

    private void Start()
    {
        UiShowCardMissing.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && keycardManager.firstkeycard == true)
        {
            door.ObjectEntered(true);
        }

        if (other.gameObject.CompareTag("Player") && keycardManager.firstkeycard == false)
        {
            UiShowCardMissing.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && keycardManager.firstkeycard == false)
        {
            UiShowCardMissing.SetActive(false);
        }
    }
}

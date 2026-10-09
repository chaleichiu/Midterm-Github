using UnityEngine;

public class Keycard : MonoBehaviour
{
    private KeycardManager keycardManager;
    private void Start()
    {
        keycardManager = FindAnyObjectByType<KeycardManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            keycardManager.firstkeycard= true;
            Destroy(gameObject);
        }
    }
}

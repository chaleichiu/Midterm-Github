using UnityEngine;

namespace Midterm
{
    public class Target : MonoBehaviour
    {
        [SerializeField] string openTag = "Bullet";
        [SerializeField] private TargetManager targetManager;

        // Call this method from your weapon/projectile script when hit
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(openTag))
            {
                DestroyTarget();
            }

        }
        public void DestroyTarget()
        {
            if (targetManager != null)
            {
                targetManager.OnTargetDestroyed();
            }

            // Disable or destroy the target object
            gameObject.SetActive(false);
            // Or: Destroy(gameObject);
        }

    }
}

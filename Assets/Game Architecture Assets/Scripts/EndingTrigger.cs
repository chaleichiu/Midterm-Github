using UnityEngine;
using UnityEngine.Events;

namespace Midterm
{
    public class EndingTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent _event;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")){
                _event.Invoke();
            }
        }
    }
}

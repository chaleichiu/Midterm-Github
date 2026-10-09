using UnityEngine;
using System.Collections;

namespace Midterm
{
        public class TargetManager : MonoBehaviour
        {
            [Header("Door Settings")]
            [SerializeField] private GameObject door;

            [Header("Indicator Lights")]
            [SerializeField] private Renderer[] lightRenderers; 
            [SerializeField] private Color lockedColor = Color.red;
            [SerializeField] private Color unlockedColor = Color.green;
            [SerializeField] Animator animator;

        private int targetsDestroyed = 0;
            private int totalTargets = 3;

            private void Start()
            {
                // Set all lights to red initially
                foreach (Renderer lightRenderer in lightRenderers)
                {
                    if (lightRenderer != null)
                    {
                        lightRenderer.material.color = lockedColor;
                    }
                }
            }

            public void OnTargetDestroyed()
            {
                if (targetsDestroyed >= totalTargets) return;

                // Turn the corresponding light green
                if (targetsDestroyed < lightRenderers.Length && lightRenderers[targetsDestroyed] != null)
                {
                    lightRenderers[targetsDestroyed].material.color = unlockedColor;
                }

                targetsDestroyed++;

                // Open the door when all targets are down
                if (targetsDestroyed >= totalTargets)
                {
                    OpenDoor();
                }
            }

            private void OpenDoor()
            {
                animator.SetBool("DoorOpen", true);
                Debug.Log("Attempting to open door");
            }
        }
    }

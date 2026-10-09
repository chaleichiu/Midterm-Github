using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        DoorState();
    }

    public bool ObjectEntered(bool state)
    {
        if (state)
        {
            isOpen= true;
        }
        else
        {
            isOpen= false;
        }
        return isOpen;
    }

    private void DoorState()
    {
        if (isOpen)
        {
            animator.SetBool("DoorOpen", true);
            animator.SetBool("DoorClose", false);
        }
        else
        {
            animator.SetBool("DoorOpen", false);
            animator.SetBool("DoorClose", true);
        }
    }

}

using UnityEngine;

public class PickupInteractor : Interactor
{
    [Header("Picked & Dropped")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private float pickupDistance;
    [SerializeField] private Transform attachTransform;

    private bool isPicked = false;
    private RaycastHit raycastHit;
    private IPickable pickable;

    public override void Interact()
    {
        Debug.Log("Attempting to pickup");
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, pickupDistance, pickupLayer))
        {
            if (input.activatePressed && !isPicked)
            {
                pickable = raycastHit.transform.GetComponent<IPickable>();
                if (pickable == null) return;

                pickable.OnPicked(attachTransform);
                isPicked = true;
                return;
            }
        }
        if (input.activatePressed && isPicked && pickable != null)
        {
            pickable.OnDropped();
            isPicked = false;
        }
    }
}

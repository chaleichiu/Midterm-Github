using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovementBehaviour : MonoBehaviour
{
    private PlayerInput input;

    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertMouse;

    private float cameraXRotation;

    void Start()
    {
        input = PlayerInput.GetInstance();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }
    void RotateCamera()
    {
        cameraXRotation += Time.deltaTime * input.mouseY * turnSpeed * (invertMouse ? 1 : -1);
        cameraXRotation = Mathf.Clamp(cameraXRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(cameraXRotation, 0, 0);
    }
}

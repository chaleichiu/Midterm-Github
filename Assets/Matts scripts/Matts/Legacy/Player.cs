using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float moveMultiplier;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float jumpVelocity;

    [Header("Ground check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance;

    [Header("Shoot")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Rigidbody rocketPrefab;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;

    [Header("Picked & Dropped")]
    [SerializeField] private LayerMask pickupLayer;
    [SerializeField] private float pickupDistance;
    [SerializeField] private Transform attachTransform;

    [Header("Interact")]
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float interactDistance;

    private CharacterController characterController;
    private Transform mainCamera;

    private bool isGrounded;
    private float horizontal, vertical;
    private float mouseX, mouseY;
    private float cameraRotation;
    private Vector3 playerVelocity;

    //Raycast
    private RaycastHit raycastHit;
    private ISelectable selectable;

    //Pick and Drop
    private bool isPicked = false;
    private IPickable pickable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // fill reference dynamicly
        mainCamera = Camera.main.gameObject.transform;

        //hide mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        GroundCheck();
        MovePlayer();
        RotatePlayer();
        JumpCheck();

        ShootBullet();
        ShootRocket();

        PickAndDrop();
        Interact();
    }

    private void GetInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        moveMultiplier = Input.GetButton("Sprint") ? sprintMultiplier : 1; // Bool check if sprint pressed, if pressed sprint multipler otherwise else 1 which is default speed
    }

    private void MovePlayer()
    {
        characterController.Move((transform.forward * vertical + transform.right * horizontal) * moveSpeed * Time.deltaTime * moveMultiplier);

        // Character controller dosen't quite touch the ground, this forces it to do that.
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        Debug.Log("Grounded: " + isGrounded);
    }

    void JumpCheck()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = jumpVelocity;
        }
    }

    private void RotatePlayer()
    {
        // Player Turn Movement
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * mouseX);

        //Camera up/down movement
        cameraRotation += Time.deltaTime * mouseY * turnSpeed * -1f;
        cameraRotation = Mathf.Clamp(cameraRotation, -80f, 80f);
        mainCamera.localRotation = Quaternion.Euler(cameraRotation, 0, 0);

    }

    void ShootBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce);
            Destroy(bullet.gameObject, 5.0f);

        }
    }

    void ShootRocket()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Rigidbody bullet = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce);
            Destroy(bullet.gameObject, 5.0f);

        }
    }

    void PickAndDrop()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, pickupDistance, pickupLayer))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isPicked)
            {
                pickable = raycastHit.transform.GetComponent<IPickable>();
                if (pickable == null) return;

                pickable.OnPicked(attachTransform);
                isPicked = true;
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && isPicked && pickable != null)
        {
            pickable.OnDropped();
            isPicked = false;
        }
    }

    void Interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, interactDistance, interactLayer))
        {

            selectable = raycastHit.transform.GetComponent<ISelectable>();

            if (selectable != null) 
            {
                selectable.OnHoverEnter();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    selectable.OnSelect();
                }
            }
        }

        if (raycastHit.transform == null && selectable != null)
        {
            selectable.OnHoverExit();
            selectable = null;
        }

    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
        }
    }
}

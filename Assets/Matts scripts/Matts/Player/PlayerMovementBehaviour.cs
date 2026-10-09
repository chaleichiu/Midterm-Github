using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementBehaviour : MonoBehaviour
{
    private PlayerInput input;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float sprintMultipler;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance;

    private CharacterController characterController;

    private Vector3 playerVelocity;
    public bool isGrounded { get; private set; }

    private float moveMultiplier = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = PlayerInput.GetInstance();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovePlayer();
    }

    void MovePlayer()
    {
        moveMultiplier = input.sprintHeld ? sprintMultipler : 1f;
        characterController.Move((transform.forward * input.vertical + transform.right * input.horizontal) * moveSpeed * Time.deltaTime * moveMultiplier);

        // Character controller dosen't quite touch the ground, this forces it to do that.
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // v = u + a * t
        playerVelocity.y += gravity * Time.deltaTime;
        // V = (1/2) * a * t^2
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }
    public void SetYVelocity(float value)
    {
        playerVelocity.y = value;
    }
    public float GetForwardSpeed()
    {
        return input.vertical * moveSpeed * moveMultiplier;
    }
}

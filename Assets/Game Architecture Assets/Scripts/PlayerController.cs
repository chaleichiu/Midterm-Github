using UnityEngine;
using UnityEngine.InputSystem;

namespace Midterm
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [Header("PlayerMovement")]
        [SerializeField] float _moveSpeed = 5f;
        [SerializeField] float _turnSpeed = 10f;
        [SerializeField] Transform _cameraTransform;
        [SerializeField] bool _invertMouse;
        [SerializeField] float _gravity = -9.81f;
        [SerializeField] float _jumpVelocity = 5f;
        [SerializeField] float _sprintMultipler = 2f;

        [Header("GroundChecks")]
        [SerializeField] Transform _groundCheck;
        [SerializeField] LayerMask _groundLayer;
        [SerializeField] LayerMask _pickableLayer;
        [SerializeField] float _groundCheckDistance;

        [Header("Shoot")]
        [SerializeField] Rigidbody _bulletPrefab;
        [SerializeField] float _shootForce;
        [SerializeField] Transform _shootPoint;


        CharacterController _characterController;

        Vector3 _playerVelocity;
        bool _isGrounded;
        float _moveMultiplier = 1f;

        Vector2 _moveInput;
        Vector2 _lookInput;
        float _camXrotation;
        bool _sprinting;
        bool _jumpPressed;
        bool _shootPressed;

        #region InputCallbacks
        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnLook(InputValue value)
        {
            _lookInput = value.Get<Vector2>();
        }

        public void OnSprint(InputValue value)
        {
            if (value.isPressed)
            {
                _sprinting = true;
            }
        }

        public void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                _jumpPressed = true;
            }
        }

        public void OnShoot(InputValue value)
        {
            if (value.isPressed)
            {
                _shootPressed = true;
            }
        }



        #endregion

        void Start()
        {
            _characterController = GetComponent<CharacterController>();

            CursorSetup();
        }

        void CursorSetup()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        // Update is called once per frame
        void Update()
        {
            RotatePlayer();
            GroundCheck();
            MovePlayer();
            if (_jumpPressed)
            {
                JumpCheck();
                _jumpPressed = false;
            }
            if (_shootPressed)
            {
                ShootBullet();
                _shootPressed = false;
            }
            Debug.Log($"Input: {_moveInput}, Speed: {_moveSpeed}");

        }
        void RotatePlayer()
        {
            transform.Rotate(Vector3.up * _lookInput.x * _turnSpeed * Time.deltaTime);
            _camXrotation += _lookInput.y * _turnSpeed * Time.deltaTime * (_invertMouse ? 1 : -1);
            _camXrotation = Mathf.Clamp(_camXrotation, -85f, 85);
            _cameraTransform.localRotation = Quaternion.Euler(_camXrotation, 0,0);
        }

        void MovePlayer()
        {
            if (_sprinting)
            {
                _moveMultiplier = _sprinting ? _sprintMultipler : 1f;
            }
            else
            {
                _moveMultiplier = 1f;
                _sprinting = false;
            }
            
            Vector3 move = transform.forward * _moveInput.y + transform.right * _moveInput.x;

            _characterController.Move(move * _moveSpeed * _moveMultiplier * Time.deltaTime);

            if (_isGrounded && _playerVelocity.y < 0)
            {
                _playerVelocity.y = -2f;
            }

            _playerVelocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }

        void GroundCheck()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckDistance, _groundLayer | _pickableLayer);
        }

        void JumpCheck()
        {
            if (_isGrounded)
            {
                _playerVelocity.y = _jumpVelocity;
            }
        }

        void ShootBullet()
        {
            Rigidbody bullet = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);
            bullet.AddForce(_shootPoint.forward * _shootForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, 5f);
        }
    }
}

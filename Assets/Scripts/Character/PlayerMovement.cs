using UnityEngine;

public class PlayerMovement : CharacterMovement
{
 
    private const string MovementHorizontalKey = "Horizontal";

    [SerializeField] private float _gravityMultiplier = 2f;
    [SerializeField] private float _movementSpeed = 6f;

    [SerializeField] private float _jumpSpeed = 30f;
    [SerializeField] private float _jumpDuration = 1f;

    private Rigidbody _rigidbody;

    private bool _isGrounded;
    private bool _isJumping;
    private float _jumpTimer;


    protected override void OnInit()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Gravity();
        if (!IsActive)
        {
            return;
        }
        Movement();
        Jumping();
    }
    private void Gravity()
    {
        if (_isGrounded)
        {
            return;
        }
        Vector3 gravity = Physics.gravity;
        gravity *= _gravityMultiplier * Time.fixedDeltaTime;
        _rigidbody.velocity += gravity;
    }

    private void Movement()
    {
        Vector3 movement = Vector3.zero;
        movement.x = Input.GetAxis(MovementHorizontalKey);
        movement = Vector3.ClampMagnitude(movement, 1f);

        movement *= _movementSpeed;

        _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        _isJumping = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        _isGrounded = true;
        _isJumping = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded && !_isJumping)
        {
            _isGrounded = false;
            _isJumping = true;
            _jumpTimer = 0;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpSpeed, 0f);
        }

        if (_isJumping)
        {
            _jumpTimer += Time.fixedDeltaTime;
            if (_jumpTimer >= _jumpDuration)
            {
                _isJumping = false;
            }
        }
    }
}

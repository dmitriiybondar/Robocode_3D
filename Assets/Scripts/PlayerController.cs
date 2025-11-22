using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _angleY, _dirZ, _jumpForce = 6f, _turnSpeed = 200f;
    private bool _isGrounded;
    private Rigidbody _rb;
    private Animator _animator;
    private Vector3 _jumpDir;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _angleY = Input.GetAxis("Mouse X") * _turnSpeed * Time.fixedDeltaTime;
        _dirZ = Input.GetAxis("Vertical");
        transform.Rotate(new Vector3(0f, _angleY, 0f));
    }

    void Update()
    {
        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            else
            {
                _animator.SetTrigger("isLanded");
            }

            Move(_dirZ, "isWalkForward", "isWalkBack");
            Sprint();
            Dodge();
        }
        else
        {
            MoveInAir();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        _animator.applyRootMotion = true;
    }

    private void Move(float dir, string parametrName, string altParametrName)
    {
        if (dir > 0)
        {
            _animator.SetBool(parametrName, true);
        }
        else if (dir < 0)
        {
            _animator.SetBool(altParametrName, true);
        }
        else
        {
            _animator.SetBool(parametrName, false);
            _animator.SetBool(altParametrName, false);
        }
    }
    
    private void Jump()
    {
        _animator.Play("Sword_Jump_Platformer_Start");
        _animator.applyRootMotion = false;
        _jumpDir = new Vector3(0f, _jumpForce, _dirZ * _jumpForce / 2f);
        _jumpDir = transform.TransformDirection(_jumpDir);
        _rb.AddForce(_jumpDir, ForceMode.Impulse);
        _isGrounded = false;
    }

    private void MoveInAir()
    {
        if (new Vector2(_rb.velocity.x, _rb.velocity.z).magnitude < 1.1f)
        {
            _jumpDir = new Vector3(0f, _rb.velocity.y, _dirZ);
            _jumpDir = transform.TransformDirection(_jumpDir);
            _rb.velocity = _jumpDir;
        }
    }

    private void Sprint()
    {
        _animator.SetBool("isRun", Input.GetKey(KeyCode.LeftShift));
    }

    private void Dodge()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _animator.Play("Sword_Dodgle_Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _animator.Play("Sword_Dodge_Right");
        }
    }
}

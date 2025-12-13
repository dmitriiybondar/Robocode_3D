using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _angleY, _dirZ, _jumpForce = 6f, _turnSpeed = 150f;
    private float _lastAttackTime; //Для автоматичного ховання меча через деякий час
    private bool _isGrounded;
    private bool _isSwordEquipped;
    private Vector3 _jumpDir;
    private Vector3 _localSwordPosition;
    private Quaternion _localSwordRotation;
    private Rigidbody _rb;
    private Animator _animator;
    [SerializeField] private GameObject sword;

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
            
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
            {
                if (!_isSwordEquipped)
                {
                    _animator.Play("Equip_Sword");
                    _lastAttackTime = Time.time; //Для автоматичного ховання меча через деякий час
                }
                else
                {
                    Attack();
                    _lastAttackTime = Time.time; //Для автоматичного ховання меча через деякий час
                }
            }

            //Автоматично сховати меч через 5 секунд
            if (_isSwordEquipped && Time.time > _lastAttackTime + 5f)
            {
                _animator.Play("Holster_Sword");
            }
            
            //Сховати меч при натисканні на колесико миші
            if (_isSwordEquipped && Input.GetMouseButton(2))
            {
                _animator.Play("Holster_Sword");
            }
            
            //Сховати меч при натисканні на кнопку клавіатури
            if (_isSwordEquipped && Input.GetKeyDown(KeyCode.R))
            {
                _animator.Play("Holster_Sword");
            }
            

            Move(_dirZ, "isWalkForward", "isWalkBackward");
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

    private void EquiptSword()
    {
        _localSwordPosition = sword.transform.localPosition;
        _localSwordRotation = sword.transform.localRotation;
        sword.transform.SetParent(GameObject.Find("RightHand").transform);
        _isSwordEquipped = true;
    }

    private void UnequiptSword()
    {
        sword.transform.SetParent(GameObject.Find("Root/Hips").transform);
        sword.transform.localPosition = _localSwordPosition;
        sword.transform.localRotation = _localSwordRotation;
        _isSwordEquipped = false;
    }

    private void Attack()
    {
        
    }
}

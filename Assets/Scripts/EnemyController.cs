using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health = 3;
    private float _prevHitTime = 0f, _ignoreDamageTime = 1.5f;
    private Animator _animator;
    
    private NavMeshAgent _agent;
    private Transform _playerTransform;
    private float _prevAttackTime, _pauseAttackWindow = 2.5f;
    [SerializeField] private Transform[] patrolTargets;
    private int _currentTargetIndex = 0;
    public bool isAttacking = false;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsName("Sword_Attack_R");
        if (health > 1)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
            if (distanceToPlayer < 2.5f)
            {
                Attack();
            }
            else if (distanceToPlayer > 30f)
            {
                Patrol();
            }
            else
            {
                MoveToPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Weapon") && Time.time > _prevHitTime + _ignoreDamageTime)
        {
            health--;
            _prevHitTime = Time.time;
            if (health > 1)
            {
                _animator.Play("KnockdownRight");
            }
            else if (health == 1)
            {
                _animator.Play("Sword_Defeat_1_Start");
            }
            else
            {
                _animator.SetTrigger("isDead");
            }
        }
    }

    private void Attack()
    {
        _animator.SetBool("isWalk", false);
        _agent.destination = transform.position;
        transform.LookAt(_playerTransform.position);

        if (Time.time > _prevAttackTime + _pauseAttackWindow &&
            !_animator.GetCurrentAnimatorStateInfo(0).IsName("KnockdownRight"))
        {
            _animator.Play(Swo);
        }
    }

    private void Patrol()
    {
        
    }

    private void MoveToPlayer()
    {
        _animator.SetBool("isWalk", true);
        _agent.destination = _playerTransform.position;
    }
}

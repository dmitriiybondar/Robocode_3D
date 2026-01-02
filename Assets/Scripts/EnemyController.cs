using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health = 3;
    private float _prevHitTime = 0f, _ignoreDamageTime = 1.5f;
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
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
}

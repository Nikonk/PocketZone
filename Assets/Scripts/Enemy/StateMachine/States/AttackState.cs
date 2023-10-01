using UnityEngine;

namespace PocketZone.Enemy.StateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AttackState : State
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _delayBetweenAttack;

        private float _lastAttackTime;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        private void Update()
        {
            if (_lastAttackTime < 0)
            {
                Attack(Target);
                _lastAttackTime = _delayBetweenAttack;
            }

            _lastAttackTime -= Time.deltaTime;
        }

        private void Attack(Player.Player target)
        {
            target.ApplyDamage(_damage);
        }
    }
}
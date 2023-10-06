using UnityEngine;

namespace PocketZone.Enemy.StateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveState : State
    {
        private const float _moveLimiter = 0.7f;

        [SerializeField] private float _speed;

        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Target == null)
                return;

            Move();
        }

        private void Move()
        {
            _moveDirection = (Target.transform.position - _transform.position).normalized;

            if (_moveDirection.x != 0 && _moveDirection.y != 0)
                _moveDirection *= _moveLimiter;

            _rigidbody.velocity = new Vector2(_moveDirection.x * _speed, _moveDirection.y * _speed);
        }
    }
}
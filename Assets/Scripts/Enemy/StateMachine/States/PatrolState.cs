using UnityEngine;

namespace PocketZone.Enemy.StateMachine
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PatrolState : State
    {
        private const float _moveLimiter = 0.7f;

        [SerializeField] private float _speed;
        [SerializeField] private float _distance;

        private Transform _transform;
        private Rigidbody2D _rigidbody;

        private Vector2 _movePoint;
        private Vector2 _moveDirection;

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
            NextMovePoint();
        }

        private void Update()
        {
            if ((Vector2)_transform.position == _movePoint)
                NextMovePoint();

            Move();
        }

        private void Move()
        {
            _moveDirection = (_movePoint - (Vector2)_transform.position).normalized;

            if (_moveDirection.x != 0 && _moveDirection.y != 0)
                _moveDirection *= _moveLimiter;

            _rigidbody.velocity = new Vector2(_moveDirection.x * _speed, _moveDirection.y * _speed);
        }

        private void NextMovePoint()
        {
            float minPositionX = _transform.position.x - _distance;
            float maxPositionX = _transform.position.x + _distance;
            float minPositionY = _transform.position.y - _distance;
            float maxPositionY = _transform.position.y + _distance;

            _movePoint.x = Random.Range(minPositionX, maxPositionX);
            _movePoint.y = Random.Range(minPositionY, maxPositionY);
        }
    }
}
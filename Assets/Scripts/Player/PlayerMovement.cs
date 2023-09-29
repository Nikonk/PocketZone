using UnityEngine;

namespace PocketZone.Player
{
    public class PlayerMovement
    {
        private const float _moveLimiter = 0.7f;

        private readonly Rigidbody2D _rigidbody;

        public PlayerMovement(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Move(float horizontal, float vertical, float speed)
        {
            if (horizontal != 0 && vertical != 0)
            {
                horizontal *= _moveLimiter;
                vertical *= _moveLimiter;
            }

            _rigidbody.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
    }
}
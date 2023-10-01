using UnityEngine;

namespace PocketZone.Enemy.StateMachine
{
    public class FindPlayerTransition : Transition
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _halfViewAngle;
        [SerializeField] private Transform _viewDirectionHandler;

        private float _currentAngle;

        private Transform _transform;

        private void Start()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (Target == null)
                return;

            if (CalculateCurrentDistance() > _distance)
                return;

            if (CalculateCurrentAngle() > _halfViewAngle * Mathf.Deg2Rad)
                return;

            NeedTransit = true;
        }

        private float CalculateCurrentDistance()
        {
            return Vector2.Distance(_transform.position, Target.transform.position);
        }

        private float CalculateCurrentAngle()
        {
            Vector2 toTargetNormalized = (Target.transform.position - _transform.position).normalized;

            return Mathf.Acos(Vector2.Dot(_viewDirectionHandler.right, toTargetNormalized));
        }
    }
}
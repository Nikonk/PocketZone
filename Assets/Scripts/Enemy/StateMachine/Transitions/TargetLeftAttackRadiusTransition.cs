using UnityEngine;

namespace PocketZone.Enemy.StateMachine
{
    public class TargetLeftAttackRadiusTransition : Transition
    {
        [SerializeField] private float _attackRadius;

        private void Start()
        {
            if (TryGetComponent(out DistanceTransition distanceTransition))
                _attackRadius = distanceTransition.MaxTransitionRange;
        }

        private void Update()
        {
            if (Target == null)
                return;

            if (Vector2.Distance(transform.position, Target.transform.position) > _attackRadius)
                NeedTransit = true;
        }
    }
}
using UnityEngine;

namespace PocketZone.Enemy.StateMachine
{
    public class DistanceTransition : Transition
    {
        [SerializeField] private float _transitionRange;
        [SerializeField] private float _rangeSpread;

        public float MaxTransitionRange => _transitionRange + _rangeSpread;

        private void Start()
        {
            _transitionRange += Random.Range(-_rangeSpread, _rangeSpread);
        }

        private void Update()
        {
            if (Target == null)
                return;

            if (Vector2.Distance(transform.position, Target.transform.position) < _transitionRange)
                NeedTransit = true;
        }
    }
}
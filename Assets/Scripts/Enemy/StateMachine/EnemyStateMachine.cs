using UnityEngine;

namespace PocketZone.Enemy.StateMachine
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyStateMachine : MonoBehaviour
    {
        [SerializeField] private State _initialState;

        private Player.Player _target;
        private State _currentState;

        private void Start()
        {
            _target = GetComponent<Enemy>().Target;
            ResetStateMachine(_initialState);
        }

        private void Update()
        {
            if (_currentState == null)
                return;

            State nextState = _currentState.GetNext();

            if (nextState != null)
                Transit(nextState);
        }

        private void Transit(State state)
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = state;
            _currentState.Enter(_target);
        }

        private void ResetStateMachine(State initialState)
        {
            _currentState = initialState;

            if (_currentState != null)
                _currentState.Enter(_target);
        }
    }
}
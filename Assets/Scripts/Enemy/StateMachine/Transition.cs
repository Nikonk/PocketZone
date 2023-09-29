using System;
using UnityEngine;

namespace PocketZone.Enemy.StateMachine
{
    public abstract class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        protected Player.Player Target { get; private set; }

        public State TargetState => _targetState;

        public bool NeedTransit { get; protected set; }

        public void Init(Player.Player target)
        {
            Target = target;
        }

        private void OnEnable()
        {
            NeedTransit = false;
        }
    }
}
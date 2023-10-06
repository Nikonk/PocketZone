using System.Collections.Generic;
using UnityEngine;

namespace PocketZone.Enemy.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;

        protected Player.Player Target { get; private set; }

        public void Enter(Player.Player target)
        {
            if (enabled == false)
            {
                Target = target;
                enabled = true;

                foreach (Transition transition in _transitions)
                {
                    transition.enabled = true;
                    transition.Init(Target);
                }
            }
        }

        public void Exit()
        {
            if (enabled)
            {
                foreach (Transition transition in _transitions)
                    transition.enabled = false;

                enabled = false;
            }
        }

        public State GetNext()
        {
            foreach (Transition transition in _transitions)
            {
                if (transition.NeedTransit)
                    return transition.TargetState;
            }

            return null;
        }
    }
}
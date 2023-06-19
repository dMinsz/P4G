using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class SymbolAI : MonoBehaviour
{
    private class IdleState : BaseState
    {
        private Transform target;
        private float range;
        private UnityEvent OnIdle;

        public IdleState(SymbolAI owner, StateMachine<State, SymbolAI> stateMachine) : base(owner, stateMachine)
        {
        }

        public override void Setup()
        {
        }

        public override void Enter()
        {
            
        }

        public override void Update()
        {

        }

        public override void Transition()
        {

            //targeting
            //if ((target.position - transform.position).sqrMagnitude < range * range)
            //{
            //    stateMachine.ChangeState(State.Trace);
            //}
        }

        public override void Exit()
        {

        }
    }
}

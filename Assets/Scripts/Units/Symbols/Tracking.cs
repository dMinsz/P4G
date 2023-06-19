using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SymbolAI : MonoBehaviour
{
    private class TrackingState : BaseState
    {
        public TrackingState(SymbolAI owner, StateMachine<State, SymbolAI> stateMachine) : base(owner, stateMachine)
        {
        }
        public override void Setup()
        {

        }

        public override void Enter()
        {
            Debug.Log("SymbolAI : Enter Tracking");

        }

        public override void Update()
        {

        }
        public override void Transition()
        {

        }

        public override void Exit()
        {

        }



    }
}

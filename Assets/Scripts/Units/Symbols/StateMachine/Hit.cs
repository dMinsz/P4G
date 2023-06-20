using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class SymbolAI : MonoBehaviour
{
    private class HitState : BaseState
    {
        private NavMeshAgent navAgent;
        private Animator anim;

        public HitState(SymbolAI owner, StateMachine<State, SymbolAI> stateMachine) : base(owner, stateMachine)
        {
        }
        public override void Setup()
        {
            navAgent = owner.agent;
            anim = owner.animator;
        }

        public override void Enter()
        {
            anim.SetTrigger("Hit");
        }

        public override void Update()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("TakeHit"))
            {
                anim.SetTrigger("Hit");
            }
        }
        public override void Transition()
        {

        }

        public override void Exit()
        {

        }



    }
}

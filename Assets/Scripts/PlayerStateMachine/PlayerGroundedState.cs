using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerGroundedState : PlayerState
    {
        public PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (Input.GetKey(KeyCode.Space)) stateMachine.ChangeState(player.jumpState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
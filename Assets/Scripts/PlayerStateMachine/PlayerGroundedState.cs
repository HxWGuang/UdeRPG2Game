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
            if (Input.GetKeyDown(KeyCode.Space) && player.GroundedCheck()) stateMachine.ChangeState(player.jumpState);
            if (Input.GetKeyDown(KeyCode.LeftShift)) stateMachine.ChangeState(player.dashState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
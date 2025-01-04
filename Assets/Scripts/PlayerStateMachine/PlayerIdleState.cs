using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.rb.velocity = new Vector2(0, 0);
        }

        public override void Update()
        {
            base.Update();

            if (_xInput == player.facingDir && player.WallCheck())
                return;
            
            if (_xInput != 0)
                stateMachine.ChangeState(player.moveState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
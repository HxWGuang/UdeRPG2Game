﻿namespace Hx.PlayerStateMachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "Move";
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            // move player
            player.SetVelocity(player.moveSpeed * _xInput, player.rb.velocity.y);

            if (_xInput == 0 || player.WallCheck())
                stateMachine.ChangeState(player.idleState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
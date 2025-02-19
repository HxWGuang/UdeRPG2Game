﻿using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerJumpState : PlayerState
    {
        public PlayerJumpState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "Jump";
        }

        public override void Enter()
        {
            base.Enter();
            player.rb.velocity = new Vector2(player.rb.velocity.x, player.jumpForce);
        }

        public override bool Update()
        {
            if (base.Update()) return true;
            if (player.rb.velocity.y != 0)
            {
                stateMachine.ChangeState(player.airState);
                return true;
            }
            
            return false;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
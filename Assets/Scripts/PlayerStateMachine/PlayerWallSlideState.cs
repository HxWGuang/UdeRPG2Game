using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "WallSlide";
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override bool Update()
        {
            if (base.Update()) return true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.ChangeState(player.wallJumpState);
                return true;
            }
            
            // 降低下滑的速度
            if (_yInput < 0)
                player.rb.velocity = new Vector2(0, player.rb.velocity.y);
            else
                player.rb.velocity = new Vector2(0, player.rb.velocity.y * .7f);

            if (_xInput != 0 && _xInput != player.facingDir)
            {
                stateMachine.ChangeState(player.idleState);
                return true;
            }

            if (player.GroundedCheck())
            {
                stateMachine.ChangeState(player.idleState);
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
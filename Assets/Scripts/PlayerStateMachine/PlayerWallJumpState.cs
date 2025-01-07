namespace Hx.PlayerStateMachine
{
    public class PlayerWallJumpState : PlayerState
    {
        public PlayerWallJumpState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "wallJump";
        }

        public override void Enter()
        {
            base.Enter();
            stateTimer = .4f;
            player.SetVelocity(5 * -player.facingDir, player.jumpForce);
        }

        public override bool Update()
        {
            if (base.Update()) return true;

            if (stateTimer < 0)
            {
                stateMachine.ChangeState(player.airState);
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
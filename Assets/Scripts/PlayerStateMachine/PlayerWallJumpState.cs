namespace PlayerStateMachine
{
    public class PlayerWallJumpState : PlayerState
    {
        public PlayerWallJumpState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.stateTimer = .4f;
            player.SetVelocity(5 * -player.facingDir, player.jumpForce);
        }

        public override void Update()
        {
            base.Update();
            
            if (player.stateTimer < 0) 
                stateMachine.ChangeState(player.airState);
            
            if (player.GroundedCheck())
                stateMachine.ChangeState(player.idleState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
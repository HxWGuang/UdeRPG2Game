namespace PlayerStateMachine
{
    public class PlayerWallSlideState : PlayerState
    {
        public PlayerWallSlideState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            
            // 降低下滑的速度
            if (_yInput < 0)
                player.SetVelocity(0, player.rb.velocity.y);
            else
                player.SetVelocity(0, player.rb.velocity.y * .7f);

            if (_xInput != 0 && _xInput != player.facingDir)
                stateMachine.ChangeState(player.idleState);

            if (player.GroundedCheck())
                stateMachine.ChangeState(player.idleState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
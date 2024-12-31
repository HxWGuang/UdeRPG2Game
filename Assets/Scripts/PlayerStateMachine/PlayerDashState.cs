namespace PlayerStateMachine
{
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            this.dashTimer = player.dashTime;
        }

        public override void Update()
        {
            base.Update();
            this.player.SetVelocity(player.dashSpeed * player.facingDir, player.rb.velocity.y);
            if (dashTimer <= 0) stateMachine.ChangeState(player.idleState);
        }

        public override void Exit()
        {
            base.Exit();
            this.player.SetVelocity(0, player.rb.velocity.y);
        }
    }
}
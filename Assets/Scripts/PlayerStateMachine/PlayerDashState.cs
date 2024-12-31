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
            player.dashTimer = player.dashDuration;
            player.dashCDTimer = player.dashCD;
        }

        public override void Update()
        {
            base.Update();
            this.player.SetVelocity(player.dashSpeed * player.dashDir, 0);
            if (player.dashTimer <= 0) stateMachine.ChangeState(player.idleState);
        }

        public override void Exit()
        {
            base.Exit();
            this.player.SetVelocity(0, player.rb.velocity.y);
        }
    }
}
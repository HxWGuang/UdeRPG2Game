namespace Hx.PlayerStateMachine
{
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.stateTimer = player.dashDuration;
            player.dashCDTimer = player.dashCD;
        }

        public override void Update()
        {
            base.Update();

            if (player.WallCheck() && !player.GroundedCheck())
                stateMachine.ChangeState(player.wallSlideState);
            
            this.player.SetVelocity(player.dashSpeed * player.dashDir, 0);
            if (player.stateTimer <= 0) stateMachine.ChangeState(player.idleState);
        }

        public override void Exit()
        {
            base.Exit();
            this.player.SetVelocity(0, player.rb.velocity.y);
        }
    }
}
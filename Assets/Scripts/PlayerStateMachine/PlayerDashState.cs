namespace Hx.PlayerStateMachine
{
    public class PlayerDashState : PlayerState
    {
        public PlayerDashState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "Dash";
        }

        public override void Enter()
        {
            base.Enter();
            stateTimer = player.dashDuration;
        }

        public override bool Update()
        {
            if (base.Update()) return true;

            if (player.WallCheck() && !player.GroundedCheck())
            {
                stateMachine.ChangeState(player.wallSlideState);
                return true;
            }
            
            this.player.SetVelocity(player.dashSpeed * player.dashDir, 0);
            if (stateTimer <= 0)
            {
                stateMachine.ChangeState(player.idleState);
                return true;
            }
            
            return false;
        }

        public override void Exit()
        {
            base.Exit();
            this.player.SetVelocity(0, player.rb.velocity.y);
        }
    }
}
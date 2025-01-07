namespace Hx.PlayerStateMachine
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "Air";
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override bool Update()
        {
            if (base.Update()) return true;
            if (_xInput != 0) player.SetVelocity(player.moveSpeed * _xInput, player.rb.velocity.y);
            player.animator.SetFloat("yVelocity", player.rb.velocity.y);

            if (_xInput != 0 && player.WallCheck())
            {
                stateMachine.ChangeState(player.wallSlideState);
                return true;
            }

            if (player.rb.velocity.y <= EPS && player.GroundedCheck())
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
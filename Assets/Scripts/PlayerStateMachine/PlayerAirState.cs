namespace PlayerStateMachine
{
    public class PlayerAirState : PlayerState
    {
        public PlayerAirState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            if (_xInput != 0) player.SetVelocity(player.moveSpeed * _xInput, player.rb.velocity.y);
            player.animator.SetFloat("yVelocity", player.rb.velocity.y);

            if (_xInput != 0 && player.WallCheck()) stateMachine.ChangeState(player.wallSlideState);
            if (player.GroundedCheck()) stateMachine.ChangeState(player.idleState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
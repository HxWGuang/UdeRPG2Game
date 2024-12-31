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
            player.animator.SetFloat("yVelocity", player.rb.velocity.y);
            
            // FIX: 这里由于GroundCheck在短时间内还是会检测到地面所以又会进入到IdleState！而不处于AirState！
            if (player.GroundedCheck()) stateMachine.ChangeState(player.idleState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
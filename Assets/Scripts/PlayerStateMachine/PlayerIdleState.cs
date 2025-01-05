namespace Hx.PlayerStateMachine
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "Idle";
        }

        public override void Enter()
        {
            base.Enter();

            player.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (_xInput == player.facingDir && player.WallCheck())
                return;
            
            if (_xInput != 0 && !player.isBusy)
                stateMachine.ChangeState(player.moveState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
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

            player.SetZeroVelocity();
        }

        public override bool Update()
        {
            if (base.Update()) return true;

            if (_xInput == player.facingDir && player.WallCheck())
                return false;

            if (_xInput != 0 && !player.isBusy)
            {
                stateMachine.ChangeState(player.moveState);
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
namespace Hx.PlayerStateMachine
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "Move";
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override bool Update()
        {
            if (base.Update()) return true;
            // move player
            player.SetVelocity(player.moveSpeed * _xInput, player.rb.velocity.y);

            if (_xInput == 0 || player.WallCheck())
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
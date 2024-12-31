namespace DefaultNamespace
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (_xInput != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
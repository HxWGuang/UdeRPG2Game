namespace DefaultNamespace
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
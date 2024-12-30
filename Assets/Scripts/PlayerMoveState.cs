namespace DefaultNamespace
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(Player player, StateMachine stateMachine, string animName) : base(player, stateMachine, animName)
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
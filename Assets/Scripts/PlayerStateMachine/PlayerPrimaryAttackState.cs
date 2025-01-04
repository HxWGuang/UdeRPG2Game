namespace Hx.PlayerStateMachine
{
    public class PlayerPrimaryAttackState : PlayerState
    {
        public PlayerPrimaryAttackState(Player player, StateMachine stateMachine, string animBoolParaName) : base(
            player, stateMachine, animBoolParaName)
        {
            player.animationEventListener.RegisterAnimationCb("PrimaryAttackEnd", OnPrimaryAttackEnd);
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

        private void OnPrimaryAttackEnd()
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
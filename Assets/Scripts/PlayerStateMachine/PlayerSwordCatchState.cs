namespace Hx.PlayerStateMachine
{
    public class PlayerSwordCatchState : PlayerState
    {
        public PlayerSwordCatchState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "SwordCatch";
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override bool Update()
        {
            return base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
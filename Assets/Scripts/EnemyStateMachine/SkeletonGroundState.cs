namespace Hx.EnemyStateMachine
{
    public class SkeletonGroundState : EnemyState
    {
        protected Skeleton enemy;
        
        public SkeletonGroundState(Enemy enemyBase, StateMachine stateMachine, string animBoolName, Skeleton _enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            stateName = "Ground";
            enemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (enemy.PlayerCheck())
                stateMachine.ChangeState(enemy.battleState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
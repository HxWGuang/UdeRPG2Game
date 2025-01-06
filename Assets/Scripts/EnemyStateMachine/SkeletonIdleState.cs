namespace Hx.EnemyStateMachine
{
    public class SkeletonIdleState : EnemyState
    {
        public Skeleton enemy;
        public SkeletonIdleState(Enemy enemyBase, StateMachine stateMachine, string animBoolName, Skeleton _enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            enemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = 2f;
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0) stateMachine.ChangeState(enemy.moveState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
namespace Hx.EnemyStateMachine
{
    public class SkeletonIdleState : SkeletonGroundState
    {
        public SkeletonIdleState(Enemy enemyBase, StateMachine stateMachine, string animBoolName, Skeleton _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
        {
            stateName = "Idle";
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = enemy.idleTime;
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
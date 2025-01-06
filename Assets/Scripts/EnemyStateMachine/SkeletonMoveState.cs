namespace Hx.EnemyStateMachine
{
    public class SkeletonMoveState : EnemyState
    {
        public Skeleton enemy;
        public SkeletonMoveState(Enemy enemyBase, StateMachine stateMachine, string animBoolName, Skeleton _enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            enemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (!enemy.GroundedCheck() || enemy.WallCheck())
            {
                enemy.Flip();
                stateMachine.ChangeState(enemy.idleState);
            }

            enemy.SetVelocity(2 * enemy.facingDir, enemy.rb.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
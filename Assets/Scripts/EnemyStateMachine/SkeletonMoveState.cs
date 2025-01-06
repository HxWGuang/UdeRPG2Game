namespace Hx.EnemyStateMachine
{
    public class SkeletonMoveState : SkeletonGroundState
    {
        public SkeletonMoveState(Enemy enemyBase, StateMachine stateMachine, string animBoolName, Skeleton _enemy) : base(enemyBase, stateMachine, animBoolName, _enemy)
        {
            stateName = "Move";
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

            enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, enemy.rb.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
using UnityEngine;

namespace Hx.EnemyStateMachine
{
    public class SkeletonAttackState : EnemyState
    {
        private Skeleton enemy;
        
        public SkeletonAttackState(Enemy enemyBase, StateMachine stateMachine, string animBoolName, Skeleton _enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            stateName = "Attack";
            enemy = _enemy;
            enemy.animationEventListener.RegisterAnimationCb("SkeletonAttackEnd", OnAttackEnd);
        }

        public override void Enter()
        {
            base.Enter();
            
            enemy.ZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();

            enemy.lastAttackTime = Time.time;
        }

        private void OnAttackEnd()
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
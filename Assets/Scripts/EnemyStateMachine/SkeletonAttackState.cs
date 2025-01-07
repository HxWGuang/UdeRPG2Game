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
            enemy.animationEventListener.RegisterAnimationCb("AttackCheck", DoAttackCheck);
            enemy.animationEventListener.RegisterAnimationCb("CounterWindowOpen", () => enemy.SetCounterWindow(true));
            enemy.animationEventListener.RegisterAnimationCb("CounterWindowClose", () => enemy.SetCounterWindow(false));
        }

        public override void Enter()
        {
            base.Enter();
            
            enemy.SetZeroVelocity();
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

        private void DoAttackCheck()
        {
            var colliders = Physics2D.OverlapCircleAll(enemy.attackCheckPoint.position, enemy.attackCheckRadius, LayerMask.GetMask("Player"));
            foreach (var collider in colliders)
            {
                collider.GetComponent<Player>().DoDamage();
            }
        }
    }
}
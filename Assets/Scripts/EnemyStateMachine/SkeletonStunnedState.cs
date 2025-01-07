using UnityEngine;

namespace Hx.EnemyStateMachine
{
    public class SkeletonStunnedState : EnemyState
    {
        private Skeleton enemy;
        public SkeletonStunnedState(Enemy enemyBase, StateMachine stateMachine, string animBoolName, Skeleton _enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            stateName = "Stunned";
            this.enemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = enemy.stunDuration;
            enemy.rb.velocity = new Vector2(enemy.stunDir.x * -enemy.facingDir, enemy.stunDir.y);
            
            enemy.compFx.blinkColor = Color.red;
            enemy.compFx.InvokeRepeating("Blink", 0, 0.1f);
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            // enemy.compFx.CancelInvoke("Blink");
            enemy.compFx.Invoke("StopBlink", 0);
        }
    }
}
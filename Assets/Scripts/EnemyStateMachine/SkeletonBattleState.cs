using UnityEngine;

namespace Hx.EnemyStateMachine
{
    public class SkeletonBattleState : EnemyState
    {
        private Skeleton enemy;
        private Transform player;
        private int moveDir;
        
        public SkeletonBattleState(Enemy enemyBase, StateMachine stateMachine, string animBoolName, Skeleton _enemy) : base(enemyBase, stateMachine, animBoolName)
        {
            stateName = "Battle";
            enemy = _enemy;
        }

        public override void Enter()
        {
            base.Enter();
            stateTimer = enemy.battleTime;
            player = GameObject.Find("Player").transform;
        }

        public override void Update()
        {
            base.Update();

            if (player.position.x > enemy.transform.position.x)
                moveDir = 1;
            else if (player.position.x < enemy.transform.position.x)
                moveDir = -1;
            
            enemy.SetVelocity(enemy.moveSpeed * moveDir, enemy.rb.velocity.y);

            var res = enemy.PlayerCheck();
            if (res)
            {
                // 发现玩家后重置需要重置battleTime
                stateTimer = enemy.battleTime;
                
                if (Vector2.Distance(enemy.transform.position, player.position) < enemy.attackRange)
                {
                    if (CanAttack())
                        stateMachine.ChangeState(enemy.attackState);
                }
            }
            else
            {
                if (stateTimer < 0 || Vector2.Distance(enemy.transform.position, player.position) > enemy.playerCheckDis)
                {
                    stateMachine.ChangeState(enemy.idleState);
                } 
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        private bool CanAttack()
        {
            if (Time.time < enemy.lastAttackTime + enemy.attackColdDown) return false;
            return true;
        }
    }
}
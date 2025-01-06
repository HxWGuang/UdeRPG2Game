using Hx.Utils;
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
            
            if (enemy.PlayerCheck().distance < enemy.attackRange)
            {
                LogUtils.Log("Skeleton Attack!");
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
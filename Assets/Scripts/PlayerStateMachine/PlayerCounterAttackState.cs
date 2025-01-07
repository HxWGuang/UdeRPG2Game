using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerCounterAttackState : PlayerState
    {
        public PlayerCounterAttackState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "CounterAttack";
            player.animationEventListener.RegisterAnimationCb("SuccessCounterAttackEnd", OnSuccessCounterAttackAnimEnd);
        }

        public override void Enter()
        {
            base.Enter();

            player.animator.SetBool("SuccessCounterAttack", false);
            stateTimer = player.counterAttackWindow;
            
            player.SetZeroVelocity();
        }

        public override void Update()
        {
            base.Update();
            
            
            
            var colliders = Physics2D.OverlapCircleAll(player.attackCheckPoint.position, player.attackCheckRadius, LayerMask.GetMask("Enemy"));
            foreach (var collider in colliders)
            {
                var enemy = collider.GetComponent<Enemy>();
                if (enemy.CheckAndDoStunnedBeforeAttack())
                {
                    stateTimer = 10;
                    player.animator.SetBool("SuccessCounterAttack", true);
                }
            }

            if (stateTimer < 0)
            {
                stateMachine.ChangeState(player.idleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void OnSuccessCounterAttackAnimEnd()
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
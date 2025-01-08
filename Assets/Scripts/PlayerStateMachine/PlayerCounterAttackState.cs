using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerCounterAttackState : PlayerState
    {
        public PlayerCounterAttackState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "CounterAttack";
            player.compAnimEventListener.RegisterAnimationCb("SuccessCounterAttackEnd", OnSuccessCounterAttackAnimEnd);
        }

        public override void Enter()
        {
            base.Enter();

            player.animator.SetBool("SuccessCounterAttack", false);
            stateTimer = player.counterAttackWindow;
            
            player.SetZeroVelocity();
        }

        public override bool Update()
        {
            if (base.Update()) return true;
            
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
                return true;
            }

            return false;
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
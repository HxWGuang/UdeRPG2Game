using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerSwordCatchState : PlayerState
    {
        public PlayerSwordCatchState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "SwordCatch";
            player.compAnimEventListener.RegisterAnimationCb("SwordCatchEnd", OnSwordCatchEnd);
        }

        public override void Enter()
        {
            base.Enter();
            
            player.SetZeroVelocity();
            
            // Hero根据Sword回来的方向决定是否翻转
            if(player.compSkillMgr.swordThrow.throwingSwrodGO.transform.position.x > player.transform.position.x && !player.facingRight)
                player.Flip();
            else if(player.compSkillMgr.swordThrow.throwingSwrodGO.transform.position.x < player.transform.position.x && player.facingRight)
                player.Flip();
            
            // 增加一些冲击力
            var impact = player.compSkillMgr.swordThrow.config.returnImpact;
            player.rb.velocity = new Vector2(-player.facingDir * impact.x, impact.y);
        }

        public override bool Update()
        {
            return base.Update();
        }

        public override void Exit()
        {
            base.Exit();

            player.StartCoroutine("BusyFor", 0.1f);
        }

        private void OnSwordCatchEnd()
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
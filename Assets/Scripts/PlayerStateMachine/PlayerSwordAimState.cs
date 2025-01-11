using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerSwordAimState : PlayerState
    {
        public PlayerSwordAimState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "SwordAim";
            player.compAnimEventListener.RegisterAnimationCb("SwordThrowEnd", OnSwordThrowEnd);
            player.compAnimEventListener.RegisterAnimationCb("SwordThrow", OnSwordThrow);
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("Enter SwordAim");
            
            player.SetZeroVelocity();
            player.compSkillMgr.swordThrow.SetDotActive(true);
        }

        public override bool Update()
        {
            if (base.Update()) return true;

            if (player.HasSwordInHand() && Input.GetKeyUp(KeyCode.Mouse1))
            {
                Debug.Log("Mouse1 up");
                player.animator.SetBool("SwordAim", false);
                // stateMachine.ChangeState(player.idleState);
                player.compSkillMgr.swordThrow.SetDotActive(false);
                return true;
            }

            // if (!isSwordThrow && Input.GetKeyDown(KeyCode.Mouse0))
            // {
            //     Debug.Log("Mouse0 pressed");
            //     isSwordThrow = true;
            //     player.animator.SetBool("SwordAim", false);
            // }

            return false;
        }

        public override void Exit()
        {
            base.Exit();
            
            Debug.Log("Exit SwordAim");
        }

        private void OnSwordThrow()
        {
            Debug.Log("OnSwordThrow");
            player.compSkillMgr.swordThrow.UseSkill();
        }
        
        private void OnSwordThrowEnd()
        {
            Debug.Log("OnSwordThrowEnd");
            stateMachine.ChangeState(player.idleState);
        }
    }
}
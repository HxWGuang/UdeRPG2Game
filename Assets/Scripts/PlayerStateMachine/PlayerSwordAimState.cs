using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerSwordAimState : PlayerState
    {
        private bool isSwordThrow;
        
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
            
            isSwordThrow = false;
            player.SetZeroVelocity();
        }

        public override bool Update()
        {
            if (base.Update()) return true;
            
            if (!isSwordThrow && Input.GetKeyUp(KeyCode.Mouse1))
            {
                Debug.Log("Mouse1 up");
                isSwordThrow = true;
                player.animator.SetBool("SwordAim", false);
                // stateMachine.ChangeState(player.idleState);
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
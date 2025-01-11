using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerSwordAimState : PlayerState
    {
        private Camera cam;
        
        public PlayerSwordAimState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player, stateMachine, animBoolParaName)
        {
            stateName = "SwordAim";
            player.compAnimEventListener.RegisterAnimationCb("SwordThrowEnd", OnSwordThrowEnd);
            player.compAnimEventListener.RegisterAnimationCb("SwordThrow", OnSwordThrow);
            
            cam = Camera.main;
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

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Debug.Log("Mouse1 up");
                player.animator.SetBool("SwordAim", false);
                player.compSkillMgr.swordThrow.SetDotActive(false);
                return true;
            }
            
            // Hero根据鼠标位置决定是否翻转
            if(cam.ScreenToWorldPoint(Input.mousePosition).x > player.transform.position.x && !player.facingRight)
                player.Flip();
            else if(cam.ScreenToWorldPoint(Input.mousePosition).x < player.transform.position.x && player.facingRight)
                player.Flip();

            return false;
        }

        public override void Exit()
        {
            base.Exit();
            
            Debug.Log("Exit SwordAim");

            player.StartCoroutine("BusyFor", 0.2f);
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
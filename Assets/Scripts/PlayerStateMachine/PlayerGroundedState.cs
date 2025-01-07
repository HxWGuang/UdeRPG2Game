using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerGroundedState : PlayerState
    {
        public PlayerGroundedState(Player player, StateMachine stateMachine, string animBoolParaName) : base(player,
            stateMachine, animBoolParaName)
        {
            stateName = "Grounded";
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                stateMachine.ChangeState(player.primaryAttackState);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) && player.GroundedCheck())
            {
                stateMachine.ChangeState(player.jumpState);
                return;
            }
            // 当通过Jump进入AirState的时候，前面几帧时间内会检测到地面从而又回到了IdleState，所以这里需要判断一下是否在地面更新下状态
            if (!player.GroundedCheck())
                stateMachine.ChangeState(player.airState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
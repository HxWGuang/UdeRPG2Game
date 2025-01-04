using Hx.Utils;
using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerPrimaryAttackState : PlayerState
    {
        private float attackWindow;
        private int combo;
        private float lastAttackTime;
        private int maxCombo = 2;

        public PlayerPrimaryAttackState(Player player, StateMachine stateMachine, string animBoolParaName) : base(
            player, stateMachine, animBoolParaName)
        {
            attackWindow = player.ComboWindow;
            player.animationEventListener.RegisterAnimationCb("PrimaryAttackEnd", OnPrimaryAttackEnd);
        }

        public override void Enter()
        {
            base.Enter();

            if (combo > maxCombo || Time.time - lastAttackTime >= attackWindow)
                combo = 0;

            player.animator.SetInteger("ComboCounter", combo);
            LogUtils.Log("Primary Attack Combo:" + combo);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();

            combo++;
            lastAttackTime = Time.time;
        }

        private void OnPrimaryAttackEnd()
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
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
            stateName = "PrimaryAttack";
            attackWindow = player.comboWindow;
            player.animationEventListener.RegisterAnimationCb("PrimaryAttackEnd", OnPrimaryAttackEnd);
            player.animationEventListener.RegisterAnimationCb("AttackCheck", DoAttackCheck);
        }

        public override void Enter()
        {
            base.Enter();
            
            if (combo > maxCombo || Time.time - lastAttackTime >= attackWindow)
                combo = 0;

            float attackDir = player.facingDir;
            // 这里需要及时更新_xInput，否则_xInput可能会是上一帧的输入
            _xInput = Input.GetAxisRaw("Horizontal");
            if (_xInput != 0)
            {
                attackDir = _xInput;
            }
            
            player.animator.SetInteger("ComboCounter", combo);
            player.SetVelocity(player.attackMovement[combo].x * attackDir, player.attackMovement[combo].y);
            
            stateTimer = .1f;
        }

        public override void Update()
        {
            base.Update();
            
            if (stateTimer < 0)
            {
                player.SetZeroVelocity();
            }
        }

        public override void Exit()
        {
            base.Exit();

            combo++;
            lastAttackTime = Time.time;
            
            // 攻击结束后进入Busy阻塞一段时间后再进入其他状态，制造停滞感
            player.StartCoroutine(nameof(player.BusyFor), .15f);
        }

        private void OnPrimaryAttackEnd()
        {
            stateMachine.ChangeState(player.idleState);
        }

        private void DoAttackCheck()
        {
            var colliders = Physics2D.OverlapCircleAll(player.attackCheckPoint.position, player.attackCheckRadius, LayerMask.GetMask("Enemy"));
            foreach (var collider in colliders)
            {
                var enemyScript = collider.GetComponent<Enemy>();
                enemyScript.DoDamage();
                // enemyScript.CheckAndDoStunnedBeforeAttack();
            }
        }
    }
}
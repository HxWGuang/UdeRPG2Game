using System.Collections;
using Hx.Module;
using Hx.PlayerStateMachine;
using UnityEngine;

namespace Hx
{
    public class Player : Entity
    {
        [Header("Attack Info")] 
        public Vector2[] attackMovement;
        
        public bool isBusy = false;
        
        [Header("Move Info")] public float moveSpeed = 10f;
        public float jumpForce = 12f;

        [Header("Dash Info")] public float dashSpeed = 25f;
        public float dashDuration = 0.2f;
        public float dashDir;

        [Header("Attack")] 
        public float comboWindow = 1f;
        public float counterAttackWindow = 0.5f;

        [Header("Debug")] 
        public string curState;
        
        // public bool isGrounded;
        
        #region State

        public StateMachine stateMachine;
        public PlayerIdleState idleState { get; private set; }
        public PlayerMoveState moveState { get; private set; }
        public PlayerJumpState jumpState { get; private set; }
        public PlayerAirState airState { get; private set; }
        public PlayerWallSlideState wallSlideState { get; private set; }
        public PlayerDashState dashState { get; private set; }
        public PlayerWallJumpState wallJumpState { get; private set; }
        public PlayerPrimaryAttackState primaryAttackState { get; private set; }
        public PlayerCounterAttackState counterAttackState { get; private set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();

            stateMachine = new StateMachine();
            idleState = new PlayerIdleState(this, stateMachine, "Idle");
            moveState = new PlayerMoveState(this, stateMachine, "Move");
            jumpState = new PlayerJumpState(this, stateMachine, "Jump");
            airState = new PlayerAirState(this, stateMachine, "Jump");
            wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
            dashState = new PlayerDashState(this, stateMachine, "Dash");
            wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
            primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
            counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        }

        protected override void Start()
        {
            base.Start();
            PlayerManager.instance.player = this;
            
            stateMachine.Init(idleState);
        }

        protected override void Update()
        {
            base.Update();
            
            // Debug.Log(stateMachine.currentState.stateName + "  " + rb.velocity.x);
            
            // isGrounded = GroundedCheck();
            stateMachine.currentState.Update();
            CheckDashInput();
        }

        private void CheckDashInput()
        {
            if (WallCheck()) return;

            if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CheckCanUse())
            {
                SkillManager.instance.dash.UseSkill();
                dashDir = Input.GetAxisRaw("Horizontal");
                if (dashDir == 0) dashDir = facingDir;
                stateMachine.ChangeState(dashState);
            }
        }

        public IEnumerator BusyFor(float _seconds)
        {
            isBusy = true;
            yield return new WaitForSeconds(_seconds);
            isBusy = false;
        }
    }
}
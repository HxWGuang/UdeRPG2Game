using System.Collections;
using Hx.PlayerStateMachine;
using Hx.Utils;
using UnityEngine;

namespace Hx
{
    public class Player : MonoBehaviour
    {
        [Header("Attack Info")] 
        public Vector2[] attackMovement;
        
        public bool isBusy = false;
        
        [Header("Move Info")] public float moveSpeed = 10f;
        public float jumpForce = 12f;

        [Header("Dash Info")] public float dashSpeed = 25f;
        public float dashDuration = 0.2f;
        public float dashCD = 1f;
        public float dashCDTimer;
        public float dashDir;

        [Header("Collision Info")] 
        [SerializeField] private Transform groundCheckPos;

        [SerializeField] private float groundCheckDis;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform wallCheckPos;
        [SerializeField] private float wallCheckDis;
        [SerializeField] private LayerMask wallLayer;

        [Header("Attack")] 
        public float ComboWindow = 1f;

        [Space]
        public int facingDir = 1;
        public bool facingRight = true;

        #region Component

        public Animator animator { get; private set; }

        public Rigidbody2D rb { get; private set; }

        public AnimationEventListener animationEventListener { get; private set; }

        #endregion

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

        #endregion

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            animationEventListener = GetComponent<AnimationEventListener>();

            stateMachine = new StateMachine();
            idleState = new PlayerIdleState(this, stateMachine, "Idle");
            moveState = new PlayerMoveState(this, stateMachine, "Move");
            jumpState = new PlayerJumpState(this, stateMachine, "Jump");
            airState = new PlayerAirState(this, stateMachine, "Jump");
            wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
            dashState = new PlayerDashState(this, stateMachine, "Dash");
            wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump");
            primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        }

        private void Start()
        {
            stateMachine.Init(idleState);
        }

        private void Update()
        {
            stateMachine.currentState.Update();
            CheckDashInput();
        }

        private void CheckDashInput()
        {
            if (WallCheck()) return;

            if (Input.GetKeyDown(KeyCode.LeftShift) && dashCDTimer <= 0)
            {
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

        public void SetVelocity(float xInput, float yInput)
        {
            rb.velocity = new Vector2(xInput, yInput);
            FlipController(xInput);
        }

        public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);

        private void FlipController(float _x)
        {
            if (_x > 0 && !facingRight) Flip();
            else if (_x < 0 && facingRight) Flip();
        }

        private void Flip()
        {
            facingDir *= -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }

        public bool GroundedCheck()
        {
            return Physics2D.Raycast(groundCheckPos.position, Vector2.down, groundCheckDis, groundLayer);
        }

        public bool WallCheck()
        {
            return Physics2D.Raycast(wallCheckPos.position, Vector3.right * facingDir, wallCheckDis, wallLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(groundCheckPos.position, Vector2.down * groundCheckDis);
            Gizmos.DrawRay(wallCheckPos.position, Vector3.right * facingDir * wallCheckDis);
        }
    }
}
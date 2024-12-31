using System;
using PlayerStateMachine;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed = 10f;
    public float jumpForce = 12f;
    public float dashSpeed = 25f;
    public float dashTime = 0.2f;
    
    [Header("Collision Info")] 
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float groundCheckDis;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheckPos;
    [SerializeField] private float wallCheckDis;
    [SerializeField] private LayerMask wallLayer;

    public int facingDir = 1;
    public bool facingRight = true;
    
    #region Component

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion

    #region State

    public StateMachine stateMachine;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }

    #endregion

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        stateMachine = new StateMachine();
        
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
    }

    void Start()
    {
        stateMachine.Init(idleState);
    }

    void Update()
    {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float xInput, float yInput)
    {
        rb.velocity = new Vector2(xInput, yInput);
        FlipController(xInput);
    }

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
    
    public bool GroundedCheck() => Physics2D.Raycast(groundCheckPos.position, Vector2.down, groundCheckDis, groundLayer);
    public bool WallCheck() => Physics2D.Raycast(wallCheckPos.position, Vector3.right * facingDir, wallCheckDis, wallLayer);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheckPos.position, Vector2.down * groundCheckDis);
        Gizmos.DrawRay(wallCheckPos.position, Vector3.right * facingDir * wallCheckDis);
    }
}

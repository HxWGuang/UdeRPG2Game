using System;
using PlayerStateMachine;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed = 10f;
    public float jumpForce = 12f;
    
    [Header("Collision Info")] 
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private float groundCheckDis;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheckPos;
    [SerializeField] private float wallCheckDis;
    [SerializeField] private LayerMask wallLayer;
    
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
    }
    
    public bool GroundedCheck() => Physics2D.Raycast(groundCheckPos.position, Vector2.down, groundCheckDis, groundLayer);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundCheckPos.position, Vector2.down * groundCheckDis);
    }
}

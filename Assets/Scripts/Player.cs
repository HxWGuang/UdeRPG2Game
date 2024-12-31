using PlayerStateMachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    public float moveSpeed = 10f;
    
    #region Component

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion

    #region State

    public StateMachine stateMachine;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    #endregion

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        stateMachine = new StateMachine();
        
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
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
}

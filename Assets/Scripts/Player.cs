using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator { get; private set; }
    
    public StateMachine stateMachine;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
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
        if (Input.GetKey(KeyCode.N))
        {
            this.stateMachine.ChangeState(moveState);
        } else if (Input.GetKey(KeyCode.M))
        {
            this.stateMachine.ChangeState(idleState);
        }
        
        stateMachine.currentState.Update();
    }
}

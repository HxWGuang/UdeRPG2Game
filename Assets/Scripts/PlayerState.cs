using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public Player player { get; private set; }
    public StateMachine stateMachine { get; private set; }
    public string animName { get; private set; }

    public PlayerState(Player player, StateMachine stateMachine, string animName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animName = animName;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}

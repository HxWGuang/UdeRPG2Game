using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public Player player { get; private set; }
    public StateMachine stateMachine { get; private set; }
    public string animBoolParaName { get; private set; }

    public PlayerState(Player player, StateMachine stateMachine, string animBoolParaName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolParaName = animBoolParaName;
    }

    public virtual void Enter()
    {
        this.player.animator.SetBool(animBoolParaName, true);
    }

    public virtual void Update()
    {
        
    }

    public virtual void Exit()
    {
        this.player.animator.SetBool(animBoolParaName, false);
    }
}

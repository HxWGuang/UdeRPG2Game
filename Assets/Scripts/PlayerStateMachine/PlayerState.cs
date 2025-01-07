using UnityEngine;

namespace Hx.PlayerStateMachine
{
    public class PlayerState
    {
        public string stateName;
        public Player player { get; private set; }
        public StateMachine stateMachine { get; private set; }
        public string animBoolParaName { get; private set; }
        protected float stateTimer;
        protected float EPS = 0.0001f;
        
        protected float _xInput;
        protected float _yInput;

        public PlayerState(Player player, StateMachine stateMachine, string animBoolParaName)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.animBoolParaName = animBoolParaName;
        }

        public virtual void Enter()
        {
            player.curState = stateName;
            this.player.animator.SetBool(animBoolParaName, true);
            this.stateTimer = 0;
        }

        public virtual void Update()
        {
            this._xInput = Input.GetAxisRaw("Horizontal");
            this._yInput = Input.GetAxisRaw("Vertical");
            this.stateTimer -= Time.deltaTime;
            player.dashCDTimer -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            this.player.animator.SetBool(animBoolParaName, false);
        }
    }
}

using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerState
    {
        public Player player { get; private set; }
        public StateMachine stateMachine { get; private set; }
        public string animBoolParaName { get; private set; }

        public float _xInput;
        protected float dashTimer;

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
            this._xInput = Input.GetAxisRaw("Horizontal");
            this.dashTimer -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            this.player.animator.SetBool(animBoolParaName, false);
        }
    }
}

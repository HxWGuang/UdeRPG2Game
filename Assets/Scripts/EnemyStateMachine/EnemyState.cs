using UnityEngine;

namespace Hx.EnemyStateMachine
{
    public class EnemyState
    {
        public string stateName;
        public Enemy enemyBase;
        public StateMachine stateMachine;
        public string animBoolName;

        protected float stateTimer;
        protected float EPS = 0.0001f;


        public EnemyState(Enemy enemyBase, StateMachine stateMachine, string animBoolName)
        {
            this.enemyBase = enemyBase;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            enemyBase.curState = stateName;
            enemyBase.animator.SetBool(animBoolName, true);
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            enemyBase.animator.SetBool(animBoolName, false);
        }
    }
}

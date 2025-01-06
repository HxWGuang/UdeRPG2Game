using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hx.EnemyStateMachine
{
    public class EnemyState
    {
        public string stateName;
        public Enemy enemy;
        public StateMachine stateMachine;
        public string animBoolName;

        protected float stateTimer;
        protected float EPS = 0.0001f;


        public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName)
        {
            this.enemy = enemy;
            this.stateMachine = stateMachine;
            this.animBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            
        }
    }
}

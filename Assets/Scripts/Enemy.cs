using Hx.EnemyStateMachine;
using UnityEngine;


namespace Hx
{
    public class Enemy : Entity
    { 
        public StateMachine stateMachine { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            stateMachine = new StateMachine();
        }

        protected override void Start()
        {
            base.Start();
            
            // stateMachine.Init();
        }

        
        protected override void Update()
        {
            base.Update();
        }
    }
}

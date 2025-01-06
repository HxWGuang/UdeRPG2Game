using Hx.EnemyStateMachine;

namespace Hx
{
    public abstract class Enemy : Entity
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
        }

        
        protected override void Update()
        {
            base.Update();
            
            stateMachine.currentState?.Update();
        }
    }
}

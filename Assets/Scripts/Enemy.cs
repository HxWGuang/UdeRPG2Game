using Hx.EnemyStateMachine;
using UnityEngine;

namespace Hx
{
    public abstract class Enemy : Entity
    {
        [Header("Move Info")] 
        public float moveSpeed;
        public float idleTime;

        [Header("Attack Info")] 
        public float playerCheckDis;
        public float attackRange;
        public float circleCheckRadius;
        public float attackColdDown;
        public float lastAttackTime;
        public float battleTime;

        [Header("Debug")] 
        public string curState;
        
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

        public virtual bool PlayerCheck()
        {
            var res0 = Physics2D.Raycast(wallCheckPos.position, Vector2.right * facingDir, playerCheckDis,
                LayerMask.GetMask("Player"));
            var res1 = Physics2D.OverlapCircle(transform.position, circleCheckRadius, LayerMask.GetMask("Player"));
            return res0 || (res1 != null);
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector2.right * facingDir * attackRange);
            Gizmos.DrawWireSphere(transform.position, circleCheckRadius);
        }
    }
}

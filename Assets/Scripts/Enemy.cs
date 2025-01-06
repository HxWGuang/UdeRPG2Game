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
        public float attackRange;

        public LayerMask PlayerLayer;
        
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

        public virtual RaycastHit2D PlayerCheck() => Physics2D.Raycast(wallCheckPos.position, Vector2.right * facingDir, 50, LayerMask.GetMask("Player"));

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector2.right * facingDir * attackRange);
        }
    }
}

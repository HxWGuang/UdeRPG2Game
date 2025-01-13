using Hx.EnemyStateMachine;
using UnityEngine;

namespace Hx
{
    public abstract class Enemy : Entity
    {
        [Header("Stun Info")]
        public float stunDuration;
        public Vector2 stunDir;
        
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
        [SerializeField] protected Renderer counterImage;
        protected bool canBeStunned;

        [Header("Debug")] 
        public string curState;
        public bool isStop = false;
        
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

            if (isStop) return;
            stateMachine.currentState?.Update();
        }

        public virtual bool PlayerCheck()
        {
            var res0 = Physics2D.Raycast(wallCheckPos.position, Vector2.right * facingDir, playerCheckDis,
                LayerMask.GetMask("Player"));
            var res1 = Physics2D.OverlapCircle(transform.position, circleCheckRadius, LayerMask.GetMask("Player"));
            return res0 || (res1 != null);
        }

        public virtual bool CheckAndDoStunnedBeforeAttack()
        {
            if (canBeStunned)
            {
                SetCounterWindow(false);
                return true;
            }
            return false;
        }

        public void SetCounterWindow(bool isOpen)
        {
            canBeStunned = isOpen;
            counterImage.enabled = isOpen;
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

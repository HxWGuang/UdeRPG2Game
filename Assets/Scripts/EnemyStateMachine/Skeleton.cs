namespace Hx.EnemyStateMachine
{
    public class Skeleton : Enemy
    {
        #region State

        public SkeletonIdleState idleState;
        public SkeletonMoveState moveState;
        public SkeletonBattleState battleState;
        public SkeletonAttackState attackState;
        public SkeletonStunnedState stunnedState;
        
        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            
            idleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
            moveState = new SkeletonMoveState(this, stateMachine, "Move", this);
            battleState = new SkeletonBattleState(this, stateMachine, "Move", this);
            attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
            stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        }

        protected override void Start()
        {
            base.Start();

            stateMachine.Init(idleState);
        }

        protected override void Update()
        {
            base.Update();
        }

        public override bool CheckAndDoStunnedBeforeAttack()
        {
            if (base.CheckAndDoStunnedBeforeAttack())
            {
                stateMachine.ChangeState(stunnedState);
                return true;
            }
            return false;
        }
    }
}
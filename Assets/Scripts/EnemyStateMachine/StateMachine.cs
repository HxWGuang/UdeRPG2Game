namespace Hx.EnemyStateMachine
{
    public class StateMachine
    {
        public EnemyState currentState;

        public void Init(EnemyState _enemyState)
        {
            currentState = _enemyState;
            currentState.Enter();
        }

        public void ChangeState(EnemyState _enemyState)
        {
            currentState?.Exit();
            currentState = _enemyState;
            currentState.Enter();
        }
    }
}

namespace Hx.PlayerStateMachine
{
    public class StateMachine
    {
        public PlayerState currentState { get; private set; }

        public void Init(PlayerState state)
        {
            this.ChangeState(state);
        }

        public void ChangeState(PlayerState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter();
        }
    }
}

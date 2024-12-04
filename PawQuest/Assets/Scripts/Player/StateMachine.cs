
public class StateMachine
{

    public State currentState;

    public void Initialize(State startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    // Update is called once per frame
    public void ChangeState(State newState)
    {
        currentState.Exit();

        currentState = newState;
        newState.Enter();
    }
}

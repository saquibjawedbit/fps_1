
public enum AiStateId
{
    ChasePlayer,
    Idle,
    ScoutArea
}


public interface AiState
{
    AiStateId GetId();
    void Enter(AiAgent agent);
    void Update(AiAgent agent);
    void Exit(AiAgent agent);
}

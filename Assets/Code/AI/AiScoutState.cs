using UnityEngine;

public class AiScoutState : AiState
{
    private int currentPathIndex = 1;

    public AiStateId GetId()
    {
        return AiStateId.ScoutArea;
    }

    public void Enter(AiAgent agent)
    {
        if(!agent.isSpider) agent.weaponIK.weight = 0;
        agent.navMeshAgent.stoppingDistance = 0;
        agent.navMeshAgent.speed = agent.config.scoutSpeed;
        agent.navMeshAgent.destination = agent.points[currentPathIndex].position;
    }

    public void Update(AiAgent agent)
    {
        float distance = Vector3.Distance(agent.transform.position, agent.points[currentPathIndex].position);
        if (distance <= 0.1f)
        {
            if (currentPathIndex == agent.points.Length - 1) currentPathIndex = 0;
            else currentPathIndex++;
            agent.navMeshAgent.SetDestination(agent.points[currentPathIndex].position);
        }

        agent.stateMachine.GetState(AiStateId.Idle)?.Update(agent);
    }

    public void Exit(AiAgent agent)
    {
        
    }
}

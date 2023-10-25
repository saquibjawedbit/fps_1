using UnityEngine;

public class AiChasePlayerState : AiState
{
    private float timer = 0.0f;

    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }

    public void Enter(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = agent.config.stoppingDistance;
        agent.navMeshAgent.speed = agent.config.chaseSpeed;
        if (!agent.isSpider) agent.weaponController.enabled = true;
    }

    public void Update(AiAgent agent)
    {
        if (!agent.navMeshAgent.isActiveAndEnabled) return;

        if (timer <= 0)
        {
            Vector3 direction = (agent.target.position - agent.navMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > Mathf.Pow(agent.config.maxDistance, 2))
            {
                if (agent.navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathPartial)
                { 
                    agent.navMeshAgent.destination = agent.target.position;
                }
            }
            timer = agent.config.maxTime;
        }
        timer -= Time.deltaTime;
    }

    public void Exit(AiAgent agent)
    {
        if(!agent.isSpider) agent.weaponController.enabled = false;
    }

}

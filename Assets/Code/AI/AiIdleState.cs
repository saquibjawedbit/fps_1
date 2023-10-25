using UnityEngine;

public class AiIdleState : AiState
{
    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }

    public void Enter(AiAgent agent)
    {
        if(agent.weaponIK != null) agent.weaponIK.weight = 0;
    }


    public void Update(AiAgent agent)
    {
        Vector3 playerDirection = agent.target.position - agent.transform.position;
        if(playerDirection.magnitude <= agent.config.maxSight)
        {
            playerDirection.Normalize();
            float targetAngle = Vector3.Angle(agent.transform.forward, playerDirection);
          
            if (Mathf.Abs(targetAngle) < agent.config.sightAngle)
            {
                RaycastHit hit;
                if(Physics.Linecast(agent.transform.position, agent.target.position, out hit, agent.config.visionMask))
                {
                    if (hit.transform.CompareTag("Player")) { agent.stateMachine.ChangeState(AiStateId.ChasePlayer); }
                }
            }
        }
    }

    public void Exit(AiAgent agent)
    {

    }
}

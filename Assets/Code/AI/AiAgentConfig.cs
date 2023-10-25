using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 0.2f;
    public float maxDistance = 1f;
    public float maxSight = 10;
    public float sightAngle = 70;
    public float firingDistance = 10;
    public float chaseSpeed = 4;
    public float scoutSpeed = 1.5f;
    public float stoppingDistance = 6f;

    public LayerMask visionMask;

    [Range(0,1)]
    public float weight = 0.42f;
}

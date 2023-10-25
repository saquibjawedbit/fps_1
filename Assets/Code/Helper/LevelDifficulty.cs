using UnityEngine;

public class LevelDifficulty : MonoBehaviour
{
    public AiAgentConfig config;
    public bool isDifficult = false;

    private void Awake()
    {
        if (!isDifficult) return;
        AiAgent[] agent = GameObject.FindObjectsOfType<AiAgent>();
        Debug.Log(agent.Length);
    }
}

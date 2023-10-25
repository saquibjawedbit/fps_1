using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private const string _movement = "movement";

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath) anim.SetFloat(_movement, agent.velocity.magnitude);
        else anim.SetFloat(_movement, 0);
    }
}

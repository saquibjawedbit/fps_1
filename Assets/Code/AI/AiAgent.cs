using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    [HideInInspector] public AiStateMachine stateMachine;
    [HideInInspector] public EnemyWeaponController weaponController;
    [HideInInspector] public WeaponIK weaponIK;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public SkinnedMeshRenderer mesh;
    [HideInInspector] public Transform target;
    [HideInInspector] public Animator anim;
    public bool isSpider = false;

    public AiAgentConfig config;
    public Transform[] points;
    public AiStateId initialState;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        weaponController = GetComponent<EnemyWeaponController>();
        anim = GetComponent<Animator>();
        weaponIK = GetComponent<WeaponIK>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.RegisterState(new AiScoutState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.maxSight);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, target.position);
    }
}

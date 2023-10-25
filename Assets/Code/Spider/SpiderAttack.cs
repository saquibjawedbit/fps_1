using UnityEngine;

public class SpiderAttack : MonoBehaviour
{

    [SerializeField] private float attackRange = 3.5f;
    [SerializeField] private float attackradius = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private int damage = 30;
    [SerializeField] private float attackDuration = 1.5f;

    private AiAgent agent;
    private float timeLeft = 0;

    private void Start()
    {
        agent = GetComponent<AiAgent>();
    
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(agent.target.position, transform.position);
        if(distance <= attackRange && timeLeft <= 0)
        {
            print("Attacking");
            agent.anim.SetTrigger("attack");
            Collider[] coll = Physics.OverlapSphere(attackPoint.position, attackradius, playerMask);
            foreach ( Collider item in coll)
            {
                if(item.CompareTag("Player"))
                {
                    PlayerController controller = item.GetComponent<PlayerController>();
                    controller?.TakeDamage(damage);
                    print("Attacked");
                    break;
                }
            }

            timeLeft = attackDuration;
        }

        timeLeft -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackradius);
    }
}

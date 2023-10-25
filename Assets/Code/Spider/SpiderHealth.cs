using UnityEngine.UI;
using UnityEngine;

public class SpiderHealth : MonoBehaviour
{
    private float health = 200;
    private bool dead = false;
    private float timeLeft = 0;

    [SerializeField] private float barDuration = 1.5f;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject canvas;

    private AiAgent agent;

    private void Start()
    {
        agent = GetComponentInParent<AiAgent>();
    }


    private void Update()
    {
        if (timeLeft <= 0) canvas.SetActive(false);

        timeLeft -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        timeLeft = barDuration;
        canvas.SetActive(true);
        health -= damage;
        if (health <= 0 && !dead) Dead();
        healthBar.fillAmount = 1 - (health / 200);
        if (agent.stateMachine.currentState != AiStateId.ChasePlayer) agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
    }

    void Dead()
    {
        agent.anim.SetTrigger("dead");
        agent.navMeshAgent.isStopped = true;
        agent.enabled = false;
        dead = true;
        Destroy(agent.gameObject, 2);
    }


}

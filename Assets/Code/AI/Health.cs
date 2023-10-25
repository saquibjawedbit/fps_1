using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private Rigidbody[] rb;
    private AiAgent agent;

    public GameObject weapon;

    [SerializeField] private float dieForce = 10f;
    [SerializeField] private float blinkIntensity = 10;
    [SerializeField] private float blinkDuration = 1;
    [SerializeField] private bool isHostage = false;

    private float blinkTimer = 0;


    private void Start()
    {
        agent = GetComponent<AiAgent>();
        rb = GetComponentsInChildren<Rigidbody>();
    }

    public void TakeDamage(int damage, Vector3 direction)
    {
        health -= damage;
        if (health <= 0 && !isHostage) { ActivateRagdoll(); PostDeathAffect(direction);}
        else if(health <=0 && isHostage) { GetComponent<Animator>().SetTrigger("Dead"); Milk.instance.GameResult(n: 1);  }

        blinkTimer = blinkDuration;
    }

    void PostDeathAffect(Vector3 direction)
    {
        AudioManager.instance.PlayerAudio(0);
        //Works After Activating Ragdoll
        GetComponent<WeaponIK>().enabled = false;
        GetComponent<EnemyWeaponController>().enabled = false;
        direction.y = 0;
        var _rb = agent.anim.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        _rb.AddForce(direction * dieForce, ForceMode.VelocityChange);
        GetComponent<AiAgent>().mesh.updateWhenOffscreen = true;

        if (weapon == null) return;

        weapon.layer = LayerMask.NameToLayer("Weapon");
        weapon.transform.SetParent(transform.parent, true);
        weapon.GetComponent<BoxCollider>().enabled = true;
        weapon.AddComponent<Rigidbody>();
        weapon = null;
        Destroy(this);
    }

    void ActivateRagdoll()
    {
        agent.anim.enabled = false;
        agent.navMeshAgent.enabled = false;
        for (int i = 0; i < rb.Length; i++)
        {
            rb[i].isKinematic = false;
        }
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (isHostage) return;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1;

        agent.mesh.material.EnableKeyword("_EMISSION");

        if (intensity > 1) { agent.mesh.material.SetColor("_EmissionColor", Color.red);}
        else agent.mesh.material.SetColor("_EmissionColor", Color.black) ;

        blinkTimer -= Time.deltaTime;
    }

    public void HeadShot(Vector3 direction)
    {
        health -= 100;
        if (health <= 0) { ActivateRagdoll(); PostDeathAffect(direction); }
    }
}

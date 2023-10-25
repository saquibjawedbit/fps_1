using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{

    private AiAgent agent;
    public Gun currentWeapon;
    private float nextTimeToFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AiAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.dead) return;
        Vector3 direction = agent.target.position - agent.transform.position;
        float angle = Vector3.Angle(agent.transform.forward, direction.normalized);
        if (direction.sqrMagnitude < Mathf.Pow(agent.config.firingDistance, 2) && angle < agent.config.sightAngle)
        {
            agent.anim.SetBool("Fire", true);
            agent.weaponIK.weight = agent.config.weight;
            if(Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / currentWeapon.fireRate;
                Fire();
            }
        }
        else
        {
            agent.anim.SetBool("Fire", false);
            agent.weaponIK.weight = 0;
        }
    }
    void Fire()
    {
        currentWeapon.gunFire.Play();
        currentWeapon._muzzleFlash.Play();
        Vector3 noise = new Vector3(Random.Range(-currentWeapon.recoilX, currentWeapon.recoilX), Random.Range(-currentWeapon.recoilY, currentWeapon.recoilY), 0);
        Rigidbody rb = Instantiate(currentWeapon.lineofFire, currentWeapon.tip.position, currentWeapon.transform.rotation).GetComponent<Rigidbody>();
        rb.velocity = currentWeapon.transform.forward * currentWeapon.bulletSpeed + noise;
    }
}

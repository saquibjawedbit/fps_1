using UnityEngine;

public enum GunType
{
    AUTOMATIC,
    BOMBER
}

public class Bullet : MonoBehaviour
{
    [SerializeField] private int impactForce = 30;
    [SerializeField] private int damage = 25;
    [SerializeField] private float blastRadie = 1.2f;
    [SerializeField] private float noiseRadius = 2f;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private GameObject blood;
    [SerializeField] private GameObject greenBlood;
    [SerializeField] private GunType gunType;
    [SerializeField] private LayerMask enemyMask;


    private Vector3 point;
    private Vector3 normal;

    private const string headS = "HEAD";
    private const string enemyS = "ENEMY";
    private const string playerS = "Player";
    private const string spiderS = "SPIDER";


    private void OnCollisionEnter(Collision collision)
    {

        point = collision.contacts[0].point;
        normal = collision.contacts[0].normal;

            if (GunType.AUTOMATIC == gunType)
            {
                //Taking Damage from both Player and enemy in Automatic Mode
                if (collision.collider.CompareTag(enemyS) || collision.collider.CompareTag(playerS))
                {
                    TakeDamageFromEnemy(collision, false, collision.collider.CompareTag(playerS));
                }
                else if (collision.collider.CompareTag(headS))
                {
                   // AudioManager.instance.PlayerAudio(0);
                    TakeDamageFromEnemy(collision, true);
                }
                else if (collision.collider.CompareTag(spiderS))
                {
                    //Taking Damage From Spider
                    SpiderHealth spiderHealth = collision.transform.GetComponent<SpiderHealth>();
                    Instantiate(greenBlood, transform.position, Quaternion.LookRotation(normal), collision.transform);
                    spiderHealth?.TakeDamage(damage);
                }
                else
                {
                    Instantiate(bulletHole, point, Quaternion.LookRotation(normal), collision.transform);
                    if (collision.rigidbody != null) collision.rigidbody.AddForce(-normal * impactForce);
                }
                Destroy(gameObject);
            }
            else
            {
                //Bombing Mode
                Instantiate(bulletHole, transform.position, Quaternion.LookRotation(normal));
                Collider[] coll = Physics.OverlapSphere(transform.position, blastRadie, enemyMask);
                foreach (Collider c in coll)
                {
                    float damageTake = Mathf.Abs(damage / (Vector3.Distance(transform.position, c.transform.position) + 1));
                    if (c.CompareTag(enemyS))
                    {
                        Instantiate(blood, c.transform.position, Quaternion.LookRotation(normal), collision.transform);
                        Health health = c.GetComponentInParent<Health>();
                        if (health != null)
                        {
                            health.TakeDamage((int)Mathf.RoundToInt(damageTake), transform.forward);
                        }
                    }
                    else if(c.CompareTag(spiderS))
                    {
                        SpiderHealth spiderHealth = c.transform.GetComponent<SpiderHealth>();
                        Instantiate(greenBlood, transform.position, Quaternion.LookRotation(normal), collision.transform);
                        spiderHealth?.TakeDamage(damageTake);
                    }
                }
                GetComponent<AudioSource>()?.Play();
                Destroy(gameObject, 2f);
            }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, noiseRadius);
    }

    void TakeDamageFromEnemy(Collision collision, bool headShot = false, bool player = false)
    {
        if(player)
        {
            Instantiate(blood, transform.position, Quaternion.LookRotation(normal), collision.transform);
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            if (controller != null) controller.TakeDamage(damage);
        }
        else
        {
            Health health = collision.transform.GetComponentInParent<Health>();
            if (health != null)
            {
                Instantiate(blood, point, Quaternion.LookRotation(normal), collision.transform);
                if (headShot) health.HeadShot(transform.forward);
                else health.TakeDamage(damage, transform.forward);
            }
        }
    }
}

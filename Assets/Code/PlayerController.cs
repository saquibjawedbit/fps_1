using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Transform groundCheck;
    public Transform holder;
    public GameObject blood;
    public GameObject deadUI;
    public Image healthStat;
    public LayerMask groundMask;

    [SerializeField] private Button button;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpHeight = 1f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float health = 100;

    private CharacterController controller;

    private Vector3 velocity;
    private Vector3 initialPos;
    public static bool dead = false;
    private bool isGrounded;

    private const string axisH = "Horizontal";
    private const string axisV = "Vertical";
    private float healthC;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        initialPos = transform.position;
        healthC = health;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead || Time.timeScale == 0) return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = SimpleInput.GetAxis(axisH);
        float vertical = SimpleInput.GetAxis(axisV);

        Vector3 direction = transform.forward * vertical + transform.right * horizontal;
        Time.timeScale = Mathf.Lerp(0.1f, 1, direction.magnitude);

        controller.Move(direction * speed * Time.deltaTime);
        
        if(button.Pressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        blood.SetActive(true);
        health -= damage;
        healthStat.fillAmount = health / healthC;
        if (health <= 0 && !dead) 
        {
            deadUI.SetActive(true); dead = true;
            AudioManager.instance.PlayerAudio(5);
            Time.timeScale = 1;
            AdsManager.adsManager.ShowIntersitialAds();
        }
    }

    public void Restore()
    {
        health = 100;
        TakeDamage(0);
        dead = false;
        transform.position = initialPos;
        deadUI.SetActive(false);
    }

}

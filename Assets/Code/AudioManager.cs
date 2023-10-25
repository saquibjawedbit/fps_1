using UnityEngine;


public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    private AudioSource audioPlayer;
    private bool done = false;

    //0 - Enemy Down, 4-Well Done, 5-Im Down, 6-Explosion
    //1 - Advancing, 2- Enemy Spotted, 3- Engaging Enemy
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private LayerMask enemyMask;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        audioPlayer = GetComponent<AudioSource>();
    }


    private void LateUpdate()
    {
        if (done) return;   
        bool enemySpotted = Physics.Raycast(Camera.main.transform.position, transform.forward, 15, enemyMask);
        if (enemySpotted) { PlayerAudio(2); Invoke("engagingEnemy", 0.2f); }
        done = enemySpotted;
    }

    void engagingEnemy()
    {
        PlayerAudio(3);
    }

    // Update is called once per frame
    public void PlayerAudio(int index = 1)
    {
        audioPlayer.clip = clips[index];
        audioPlayer.Play();
    }
}

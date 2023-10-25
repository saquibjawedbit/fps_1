using UnityEngine;
using UnityEngine.SceneManagement;

public class Milk : MonoBehaviour
{
    public GameObject levelUI;
    public GameObject lostUI;
    [SerializeField] private int ncctv = 0;

    public static Milk instance;
    private void Start()
    {
        instance = this;
    }

    public void SubCCTV()
    {
        ncctv -= 1;
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) GameResult();
    }

    public  void GameResult(int n = 0)
    {
        ncctv += n;
        if (ncctv == 0)
        {
            int currentlevelIndex = PlayerPrefs.GetInt("LEVEL", 1);
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            levelUI.SetActive(true);
            if (currentlevelIndex == currentLevel)
            {
                PlayerPrefs.SetInt("LEVEL", currentLevel + 1);
            }
            AudioManager.instance.PlayerAudio(4);
        }
        else
        {
            lostUI.SetActive(true);
            if (ncctv > 0) lostUI.GetComponentInChildren<LanguageManager>().SetText(sentnceIndex: 2);
            AdsManager.adsManager.ShowIntersitialAds();
        }
        Time.timeScale = 0.0001f;
    }

}

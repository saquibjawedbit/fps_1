using UnityEngine;

public class AdsManager : MonoBehaviour
{

    public static AdsManager adsManager;

    private RewardedAd rewardedAds;
    private IntersitialAd IntersitialAd;
    private int n = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (adsManager == null) adsManager = this;
        rewardedAds = GetComponent<RewardedAd>();
        IntersitialAd = GetComponent<IntersitialAd>();
    }

    public void ShowIntersitialAds()
    {
        int odds = Random.Range(-3, 3);
        if (odds >= 0) IntersitialAd.ShowAd();
    }

    public void ShowRewardedAds()
    {
        if (n > 0) return;
        rewardedAds.ShowAd();
        n++;

    }
}

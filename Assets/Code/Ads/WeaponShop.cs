using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public GameObject[] weapon;
    private int currentIndex = 0;
    public GunReward rewardAds;


    // Start is called before the first frame update
    public void NextWeapon()
    {
        weapon[currentIndex].SetActive(false);

        if (currentIndex == 0) currentIndex = 1;
        else currentIndex = 0;
        weapon[currentIndex].SetActive(true);
        
    }

    public void EquipWeapon()
    {
        rewardAds.ShowAd(currentIndex);
    }
}

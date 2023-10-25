using System;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;


public class GunReward : MonoBehaviour
{
    IRewardedAd ad;
    string adUnitId = "Gun_Rewarded";
    string gameId = "4892092";
    public UnityEngine.UI.Button button;
    public Text text;
    public Text text1;

    private int weaponIndex;

    private void Awake()
    {
        InitServices();
    }

    public async void InitServices()
    {
        try
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetGameId(gameId);
            await UnityServices.InitializeAsync(initializationOptions);

            InitializationComplete();
        }
        catch (Exception e)
        {
            InitializationFailed(e);
        }
    }

    public void SetupAd()
    {
        //Create
        ad = MediationService.Instance.CreateRewardedAd(adUnitId);

        //Subscribe to events
        ad.OnClosed += AdClosed;
        ad.OnClicked += AdClicked;
        ad.OnLoaded += AdLoaded;
        ad.OnFailedLoad += AdFailedLoad;
        ad.OnUserRewarded += UserRewarded;

        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
    }


    public async void ShowAd(int weapon_Index)
    {
        if (weapon_Index == 0) weaponIndex = 1;
        else weaponIndex = 3;
        if (ad.AdState == AdState.Loaded)
        {
            try
            {
                S2SRedeemData s2SRedeemData = new S2SRedeemData();
                s2SRedeemData.UserId = gameId;
                s2SRedeemData.CustomData = "CustomData";
                RewardedAdShowOptions showOptions = new RewardedAdShowOptions();
                showOptions.S2SData = s2SRedeemData;
                showOptions.AutoReload = true;
                await ad.ShowAsync(showOptions);
                AdShown();
            }
            catch (ShowFailedException e)
            {
                AdFailedShow(e);
            }
        }
    }

    void InitializationComplete()
    {
        SetupAd();
        LoadAd();
    }

    async void LoadAd()
    {
        try
        {
            await ad.LoadAsync();
        }
        catch (LoadFailedException)
        {
            // We will handle the failure in the OnFailedLoad callback
        }
    }

    void InitializationFailed(Exception e)
    {
        Debug.Log("Initialization Failed: " + e.Message);
        text1.text = "Initialization Failed: " + e.Message;
    }

    void AdLoaded(object sender, EventArgs e)
    {
        Debug.Log("Ad loaded");
    }

    void AdFailedLoad(object sender, LoadErrorEventArgs e)
    {
        Debug.Log("Failed to load ad");
        Debug.Log(e.Message);
        text1.text = "Failed to load ad" + e.Message;
    }

    void AdShown()
    {
        Debug.Log("Ad shown!");
    }

    void AdClosed(object sender, EventArgs e)
    {
        Debug.Log("Ad has closed");
        // Execute logic after an ad has been closed.
      //  text1.text = "Ad has closed";
    }

    void AdClicked(object sender, EventArgs e)
    {
        Debug.Log("Ad has been clicked");
        // Execute logic after an ad has been clicked.
        //text1.text = "Ad has been clicked";
    }

    void AdFailedShow(ShowFailedException e)
    {
        Debug.Log(e.Message);
        text1.text = e.Message;
    }

    void ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
    }

    void UserRewarded(object sender, RewardEventArgs e)
    {
        Debug.Log($"Received reward: type:{e.Type}; amount:{e.Amount}");
        PlayerPrefs.SetInt("WEAPON", weaponIndex);
        button.interactable = false;
        text.text = "EQUIPED";
        text1.text = "Weapon Purchased Successfully";
    }

}
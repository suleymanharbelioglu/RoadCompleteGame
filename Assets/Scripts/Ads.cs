using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class Ads : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // character
    public Character _character;

    [SerializeField] string _AndoridGameId;
    [SerializeField] string _IOSGameId;
    string _gameId;

    public bool _testMode = true;

     
    private void Awake() {
        if(Advertisement.isInitialized)
        {
            Debug.Log("ads initialized");

        }
        else
        {
            InitializeAds();
        }
    }


    //Initialize Listener
    public void OnInitializationComplete()
    {
        Debug.Log("OnInitializationComplete");
       
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("OnInitializationFailed");
       
    }


    // load listener 
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsAdLoaded");
        ShowAds(placementId);
       
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("OnUnityAdsFailedToLoad");
       
    }
    

    //show listener
    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");
       
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete");
        _character.AfterAdsGO();

       
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
       
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");
       
    }


    // methods 

    public void InitializeAds()
    {
        AssignGameId();
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void AssignGameId()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _IOSGameId : _AndoridGameId;
    }
    public void LoadInterstialAds()
    {
        Advertisement.Load("Interstitial_Android", this);
        
    }
    public void LoadRewardedAds()
    {
        Advertisement.Load("Rewarded_Android",this);
    }
    public void ShowAds(string placementId)
    {
        Advertisement.Show(placementId, this);
        
    }

}

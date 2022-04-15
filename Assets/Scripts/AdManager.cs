using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapsellSDK;
public class AdManager : MonoBehaviour
{
    private float t;
    private static AdManager m_instance;
    private bool adWatched;

    public static AdManager M_Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new AdManager();
            }

            return m_instance;
        }
    }

    private readonly string TAPSELL_KEY = "tkajeogkrqigaakhebrreasqrpqlgqchiqmenabschbefgjojrmkficprojrijlepttdls";
    private readonly string ZONE_ID = "5e89c4359867db0001bfdf1f";
    public static TapsellAd ad;
    // Start is called before the first frame update
    void Start()
    {
        Tapsell.Initialize(TAPSELL_KEY);
        Tapsell.SetRewardListener (
            (TapsellAdFinishedResult result) => {
                Debug.Log (
                    "adId:" + result.adId + ", " +
                    "zoneId:" + result.zoneId + ", " +
                    "completed:" + result.completed + ", " +
                    "rewarded:" + result.rewarded);
            }
        );
    }

    public void Request () {
       
        Tapsell.RequestAd (ZONE_ID, true,
            (TapsellAd result) => {
                // onAdAvailable
          

                Debug.Log ("on Ad Available");
                ad = result;
               UIManager.M_Instance.CloseAllMenues();
                Show();
            },

            (string zoneId) => {
                // onNoAdAvailable
                Debug.Log ("no Ad Available");
               //     UIManager.M_Instance.StartGame();
               UIManager.M_Instance.GameoverNoInternetAds();
               UIManager.M_Instance.SetAdError("no Ad Available");

            },

            (TapsellError error) => {
                // onError
                Debug.Log (error.message);
                UIManager.M_Instance.GameoverNoInternetAds();
                UIManager.M_Instance.SetAdError("Unexpected Error!");

            },

            (string zoneId) => {
                // onNoNetwork
                Debug.Log ("no Network");

UIManager.M_Instance.GameoverNoInternetAds();
UIManager.M_Instance.SetAdError("no Network");

            },

            (TapsellAd result) => {
                // onExpiring
                Debug.Log ("expiring");
                UIManager.M_Instance.GameoverNoInternetAds();
                UIManager.M_Instance.SetAdError("no Ads Available");

            },

            (TapsellAd result) => {
                // onOpen
               
                Debug.Log ("open");
            },

            (TapsellAd result) => {
                // onClose
                
                Debug.Log ("close");
            }
            

        );
        Tapsell.SetRewardListener( (TapsellAdFinishedResult result) =>
            {

                if (result.completed && result.rewarded)
                {
                    UIManager.M_Instance.Pausegame();
                }

                // you may give rewards to user if result.completed and
                // result.rewarded are both true
            }
        );
    }

  

    public void Show () {
        TapsellShowOptions showOptions = new TapsellShowOptions ();
        showOptions.backDisabled = true;
        showOptions.immersiveMode = false;
        showOptions.rotationMode = TapsellShowOptions.ROTATION_UNLOCKED;
        showOptions.showDialog = true;
        Tapsell.ShowAd (ad, showOptions);
        /*UIManager.M_Instance.WatchAdBtn.SetActive(true);
        UIManager.M_Instance.LoadingText.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}

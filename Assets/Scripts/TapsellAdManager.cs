using UnityEngine;
using TapsellSDK;
using UnityEngine.Events;

public class TapsellAdManager : MonoBehaviour
{
    private const string KEY = "tkajeogkrqigaakhebrreasqrpqlgqchiqmenabschbefgjojrmkficprojrijlepttdls";
    private const string video_ads_id = "5e89c4359867db0001bfdf1f";

    #region Singleton
    private static TapsellAdManager m_instance;
    public static TapsellAdManager Instance
    {
        get
        {
            if(!m_instance)
            {
                m_instance = new GameObject("TapsellAdManager").AddComponent<TapsellAdManager>();
                m_instance.Init();
            }
            return m_instance;
        }
    }
    #endregion

    private UnityAction rewardAction,failed;
    private bool gettingVideoAds;

    private void Init()
    {
        Tapsell.Initialize(KEY);
        Tapsell.SetRewardListener((TapsellAdFinishedResult result) =>
        {
            if(rewardAction != null)
            {
                rewardAction();
                rewardAction = null;
            }
        });
    }

    public void PlayAd(UnityAction reward,UnityAction failed)
    {
        if (gettingVideoAds) return;

        gettingVideoAds = true;

        rewardAction = reward;
        this.failed = failed;

      //  AnalyticsManager.RequestVideoAds();

        Tapsell.RequestAd(video_ads_id, true, ad =>
          {
             // AnalyticsManager.PlayVideoAds();
              gettingVideoAds = false;
              Tapsell.ShowAd(ad,new TapsellShowOptions() {backDisabled = true,rotationMode = TapsellShowOptions.ROTATION_UNLOCKED });
          }, msg => Failed(), err => Failed(), err => Failed(), ad => Failed());
    }

    private void Failed()
    {
       // AnalyticsManager.FailedVideoAds();
        gettingVideoAds = false;

        if (failed != null) failed();
        failed = null;
        
    }
}

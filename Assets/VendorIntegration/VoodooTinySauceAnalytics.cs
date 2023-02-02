using Alkacom.SDK.Analytics;

#if AK_ADDON_VOODOO_TINY_SAUCE
namespace Alkacom.Scripts.Vendor
{
    public class VoodooTinySauceAnalytics : IAnalytics
    {
        public void SendProgression(ProgressionStatus status, string levelId, int score)
        {
            switch (status)
            {
                case ProgressionStatus.Start:
                    TinySauce.OnGameStarted(levelId);
                    break;
                case ProgressionStatus.Win:
                    TinySauce.OnGameFinished(true, score, levelId);
                    break;
                case ProgressionStatus.Fail:
                    TinySauce.OnGameFinished(false, score, levelId);
                    break;
                    
            }
        }

        public void SendProgression(ProgressionStatus status, string levelId)
        {
            switch (status)
            {
                case ProgressionStatus.Start:
                    TinySauce.OnGameStarted(levelId);
                    break;
                case ProgressionStatus.Win:
                    TinySauce.OnGameFinished(true, 0, levelId);
                    break;
                case ProgressionStatus.Fail:
                    TinySauce.OnGameFinished(false, 0, levelId);
                    break;
                    
            }
        }

        public void SendAd(AdType adType, AdAction action, string placementId)
        {
            throw new System.NotImplementedException();
        }

        public void SendAd(AdType adType, AdAction action, AdError error, string placementId)
        {
            throw new System.NotImplementedException();
        }

        public void SendBusiness(string currency, int amount, string itemId, string location, IBusinessValidation validation)
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif
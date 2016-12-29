using TalentAgileShop.Model;

namespace TalentAgileShop.Web
{
    public static class FeatureSetConfig
    {

        

        public static void Initialize(FeatureSet featureSet)
        {
            featureSet.AllowThumbnailView = false;
        }
    }
}
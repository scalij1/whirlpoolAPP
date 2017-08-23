using Android.OS;
using Android.Support.V4.App;
using Android.Views;

using ApiAiSDK;
using ApiAi.Common;
using ApiAi.Android;
using Newtonsoft.Json;

using Android.Widget;
using Android.Util;


namespace whirlpoolAPP.Fragments
{
    public class Fragment2 : Fragment
    {

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //var config = new AIConfiguration("accessToken", SupportedLanguage.English);
            //aiService = AIService.CreateService(config);

            //aiService.OnResult += AiService_OnResult;
            //aiService.OnError += AiService_OnError;
        }

        public static Fragment2 NewInstance()
        {
            var frag2 = new Fragment2 { Arguments = new Bundle() };
            return frag2;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.fragment2, null);
        }
    }
}
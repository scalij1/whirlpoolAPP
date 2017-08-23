using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Google.Cloud.Speech.V1;
using System;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using System.Threading;
using System.Net.Http;
 using Android.Webkit;

namespace whirlpoolAPP.Fragments
{
    public class Fragment1 : Fragment
    {
        WebView web_view;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Task<string> cred =  AuthenticateAsync();
            //System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "/storage/9016-4EF8/config/brastempinho-f87474dab957.json");
            //var speech = SpeechClient.Create();
            //var response = speech.Recognize(new RecognitionConfig()
            //{
            //    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
            //    SampleRateHertz = 16000,
            //    LanguageCode = "en",
            //}, RecognitionAudio.FromFile("audio.raw"));
            //foreach (var result in response.Results)
            //{
            //    foreach (var alternative in result.Alternatives)
            //    {
            //        Console.WriteLine(alternative.Transcript);
            //    }
            //}
        }

        public static Fragment1 NewInstance()
        {
            var frag1 = new Fragment1 { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment1, container, false);
            //SetContentView(Resource.Layout.Fragment1);

            web_view = view.FindViewById<WebView>(Resource.Id.webView1);
            web_view.Settings.JavaScriptEnabled = true;
            //web_view.SetWebViewClient(new Fragment1());
            web_view.LoadUrl("https://webchat.botframework.com/embed/whirlpoolbot?s=DJBqU6E7PUY.cwA.Zfs.vtSbFYQMtQjAzIGvv_sYNBIVCXEoZdNj8DWzo9dgdQU");
            return view;

        }


        public async Task<string> AuthenticateAsync()
        {
            try
            {
                string filePath = Path.Combine("/storage/9016-4EF8/config/brastempinho-f87474dab957.json");
                
                byte[] authBytes = null;
                FileStream fs = new FileStream(filePath,
                                               FileMode.Open,
                                               FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = new FileInfo(filePath).Length;
                authBytes = br.ReadBytes((int)numBytes);

                //byte[] authBytes = await   ileAsync(filePath).ConfigureAwait(false);
                string authString = Encoding.ASCII.GetString(authBytes);

                GoogleCredential credential = GoogleCredential.FromJson(authString);
                credential = credential.CreateScoped("https://www.googleapis.com/auth/cloud-platform");

                string token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync().ConfigureAwait(false);
                
                return token;
            }
            catch (Exception e)
            {

                return e.Message;
            }
  
        }
      
    }
}
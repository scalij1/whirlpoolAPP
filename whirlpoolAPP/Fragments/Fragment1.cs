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

namespace whirlpoolAPP.Fragments
{
    public class Fragment1 : Fragment
    {

        

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Task<string> cred =  AuthenticateAsync();
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "/storage/9016-4EF8/config/brastempinho-f87474dab957.json");
            var speech = SpeechClient.Create();
            var response = speech.Recognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = "en",
            }, RecognitionAudio.FromFile("audio.raw"));
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine(alternative.Transcript);
                }
            }
        }

        public static Fragment1 NewInstance()
        {
            var frag1 = new Fragment1 { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            return inflater.Inflate(Resource.Layout.fragment1, null);

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
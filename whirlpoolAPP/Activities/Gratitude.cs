
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;

namespace whirlpoolAPP.Activities
{
    [Activity(Label = "Whirlpool Gratitude", Icon = "@drawable/icon")]
    public class Gratitude : BaseActivity
    {

        protected override int LayoutResource
        {
            get { return Resource.Layout.gratitude; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            listacolaboradores();
        }
        protected void listacolaboradores()
        {
            var levels = new List<String>() { "jefferson_s_pecanha@whirlpool.com", "acursio_maia@whirlpool.com", "bruno_c_castanho@whirlpool.com" };
            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, levels);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            var spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            spinner.Adapter = adapter;
           
            spinner.ItemSelected += (sender, e) =>
            {
                var s = sender as Spinner;
                Toast.MakeText(this, "Você irá agradecer: " + s.GetItemAtPosition(e.Position), ToastLength.Short).Show();
            };
        }
    }
}


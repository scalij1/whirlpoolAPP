using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;

using whirlpoolAPP.Fragments;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Auth0.OidcClient;
using Android.Content;
//using Auth0.OidcClient;
using IdentityModel.OidcClient;
using Android.Graphics;
using System.Net;
using System;
using Android.Runtime;

using Android.Text.Method;
using System.Text;

namespace whirlpoolAPP.Activities
{
    [Activity(Label = "Whirlpool Mobile", LaunchMode = LaunchMode.SingleTask, Icon = "@drawable/Icon")]
    [IntentFilter(
    new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "com.refractored.navdrawer.samplecompat",
    DataHost = "whirlpool-lar.auth0.com",
    DataPathPrefix = "/android/com.refractored.navdrawer.samplecompat/callback")]

    public class MainActivity : BaseActivity
    {
        private Auth0Client client;
        private TextView userDetailsTextView;
        private TextView txtUsername;
        private ImageView imgFoto;
        private TextView txtUsernameHome;
        private ImageView imgFotoHome;
        private AuthorizeState authorizeState;
        ProgressDialog progress;

     
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        protected override async void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            txtUsername = FindViewById<TextView>(Resource.Id.txtuser);
            txtUsername.MovementMethod = new ScrollingMovementMethod();
            txtUsername.Text = String.Empty;

            txtUsernameHome = FindViewById<TextView>(Resource.Id.txtuserHome);
            txtUsernameHome.Text = String.Empty;

            imgFoto = FindViewById<ImageView>(Resource.Id.imgfoto);
            imgFotoHome = FindViewById<ImageView>(Resource.Id.imgfotoHome);

            var loginResult = await client.ProcessResponseAsync(intent.DataString, authorizeState);

            var sb = new StringBuilder();
            if (loginResult.IsError)
            {
                sb.AppendLine($"An error occurred during login: {loginResult.Error}");
            
            }
            else
            {
                progress.Cancel();

                // sb.AppendLine($"ID Token: {loginResult.IdentityToken}");
                // sb.AppendLine($"Access Token: {loginResult.AccessToken}");
                //sb.AppendLine($"Refresh Token: {loginResult.RefreshToken}");

                //sb.AppendLine();
                //sb.AppendLine("-- Claims --");

                foreach (var claim in loginResult.User.Claims)
                {
                    //sb.AppendLine($"{claim.Type} = {claim.Value}");
                    if (claim.Type == "name")
                    {
                        txtUsername.Text = claim.Value;
                        txtUsernameHome.Text = claim.Value;
                    }
              

                    if (claim.Type == "picture")
                    {
                        Koush.UrlImageViewHelper.SetUrlDrawable(imgFoto, claim.Value);
                        Koush.UrlImageViewHelper.SetUrlDrawable(imgFotoHome, claim.Value);
                    }
                       
                }        
            }

            //userDetailsTextView.Text = sb.ToString();
        }


 
        private  Bitmap GetImageBitmapFromUrl(string url)
        {
             Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.main;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

           // SetContentView(Resource.Layout.main);

            userDetailsTextView = FindViewById<TextView>(Resource.Id.UserDetailsTextView);
            userDetailsTextView.MovementMethod = new ScrollingMovementMethod();
            userDetailsTextView.Text = String.Empty;

            client = new Auth0Client(new Auth0ClientOptions
            {
                Domain = "whirlpool-lar.auth0.com",
                ClientId = "b1tgAtm7J5xRS7bA2_vQMEH9BLVpbQuy",
                Activity = this
            });

            SignIn();

            drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            //setup navigation view
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //handle navigation
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home_1:
                        ListItemClicked(0);
                        break;
                    //case Resource.Id.nav_home_2:
                    //    ListItemClicked(1);
                    //    break;
                }

                Snackbar.Make(drawerLayout, "You selected: " + e.MenuItem.TitleFormatted, Snackbar.LengthLong)
                    .Show();

                drawerLayout.CloseDrawers();
            };


            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
               // ListItemClicked(0);
            }
        }

        private async void SignIn()
        {
            userDetailsTextView.Text = "";

            progress = new ProgressDialog(this);
            progress.SetTitle("Log In");
            progress.SetMessage("Please wait while redirecting to login screen...");
            progress.SetCancelable(false); // disable dismiss by tapping outside of the dialog
            progress.Show();

            // Prepare for the login
            authorizeState = await client.PrepareLoginAsync();

            // Send the user off to the authorization endpoint
            var uri = Android.Net.Uri.Parse(authorizeState.StartUrl);
            var intent = new Intent(Intent.ActionView, uri);
            intent.AddFlags(ActivityFlags.NoHistory);
            StartActivity(intent);
        }
      
        int oldPosition = -1;
        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == oldPosition)
                return;

            oldPosition = position;

            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    //fragment = gratitute.NewInstance();
                    StartActivity(new Intent(Application.Context, typeof(Gratitude)));
                    break;
                case 1:
                    fragment = Fragment2.NewInstance();
                    break;
            }

            //SupportFragmentManager.BeginTransaction()
            //    .Replace(Resource.Id.content_frame, fragment)
            //    .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}


using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;

namespace Diet
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button accept;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            accept = FindViewById<Button>(Resource.Id.accept_button);
            accept.Click += NextLayout;
        }

        /// <summary>
        /// Switch to the next layout with choosing the function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextLayout(object sender, EventArgs e)
        {
            Intent next_activity = new Intent(this, typeof(Start));
            StartActivity(next_activity);
        }
    }
}
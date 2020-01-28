using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Diet
{
    [Activity(Label = "GetDiet")]
    public class GetDiet : Activity
    {
        Button back;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.getDiet);
            FindViewById<TextView>(Resource.Id.calories).Text += Intent.GetStringExtra("calories" ?? "Not recv");
            FindViewById<TextView>(Resource.Id.bname).Text += Intent.GetStringExtra("bname" ?? "Not recv");
            FindViewById<TextView>(Resource.Id.btime).Text += Intent.GetStringExtra("btime" ?? "Not recv");
            FindViewById<TextView>(Resource.Id.lname).Text += Intent.GetStringExtra("lname" ?? "Not recv");
            FindViewById<TextView>(Resource.Id.ltime).Text += Intent.GetStringExtra("ltime" ?? "Not recv");
            FindViewById<TextView>(Resource.Id.dname).Text += Intent.GetStringExtra("dname" ?? "Not recv");
            FindViewById<TextView>(Resource.Id.dtime).Text += Intent.GetStringExtra("dtime" ?? "Not recv");
            back = FindViewById<Button>(Resource.Id.backtomenu);
            back.Click += BackToMenu;
        }

        /// <summary>
        /// Return to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BackToMenu(object sender, EventArgs e)
        {
            Intent menu = new Intent(this, typeof(Start));
            StartActivity(menu);
        }
    }
}
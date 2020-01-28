using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Diet
{
    [Activity(Label = "Start")]
    public class Start : Activity
    {
        Button imt, calc;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.start_layout);
            imt = FindViewById<Button>(Resource.Id.imt);
            calc = FindViewById<Button>(Resource.Id.calc);
            imt.Click += IMTLayout;
            calc.Click += CalcLayout;
        }

        /// <summary>
        /// Go to searching for a diet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void IMTLayout(object sender, EventArgs e)
        {
            Intent next_activity = new Intent(this, typeof(SetParams));
            StartActivity(next_activity);
        }

        /// <summary>
        /// Go to searching food calorie content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CalcLayout(object sender, EventArgs e)
        {
            Intent next_activity = new Intent(this, typeof(CalcSet));
            StartActivity(next_activity);
        }
    }
}
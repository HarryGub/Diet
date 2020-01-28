using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Diet
{
    [Activity(Label = "CalcSet")]
    public class CalcSet : Activity
    {
        string name;
        int amount;
        Button enter, backtomenu, reset;
        int kcal;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.calcSet);
            enter = FindViewById<Button>(Resource.Id.Enter);
            backtomenu = FindViewById<Button>(Resource.Id.backtomenu);
            reset = FindViewById<Button>(Resource.Id.reset);
            if (int.TryParse(Intent.GetStringExtra("kcal" ?? "Not recv"), out kcal) && kcal != 0)
            {
                FindViewById<TextView>(Resource.Id.totalcal).Text = "Общая калорийность: " + kcal;
            }
            enter.Click += EnterParams;
            reset.Click += Reset;
            backtomenu.Click += BackToMenu;
        }

        /// <summary>
        /// Reset amount of calorie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Reset(object sender, EventArgs e)
        {
            kcal = 0;
            FindViewById<TextView>(Resource.Id.totalcal).Text = "Общая калорийность: 0";
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

        /// <summary>
        /// Go to choosing the brand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EnterParams(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(FindViewById<EditText>(Resource.Id.Amount).Text, out amount) && amount > 0 && amount<1000)
                {
                    if (FindViewById<TextView>(Resource.Id.Name).Text == "")
                        Toast.MakeText(this, "Введите название продукта", ToastLength.Long).Show();
                    else
                    {
                        name = FindViewById<TextView>(Resource.Id.Name).Text;

                        Intent nextActivity = new Intent(this, typeof(ChooseBrand));
                        nextActivity.PutExtra("amount", amount);
                        nextActivity.PutExtra("item_name", name);
                        nextActivity.PutExtra("kcal", kcal);
                        StartActivity(nextActivity);
                    }
                }
                else
                    Toast.MakeText(this, "Введите кол-во порций продукта больше 0 и меньше 1000", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "" + ex.Message, ToastLength.Long).Show();
            }
        }
    }
}
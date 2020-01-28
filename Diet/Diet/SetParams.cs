using System;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Diet.Model;
using EDMTDialog;
using Newtonsoft.Json;

namespace Diet
{
    [Activity(Label = "SetParams")]
    public class SetParams : Activity
    {
        Button enter;
        RadioButton male, female;
        Meals meals;
        double cal;
        int weight, age, height;
        bool gender = true;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.setParams);
            enter = FindViewById<Button>(Resource.Id.enter_button);
            male = FindViewById<RadioButton>(Resource.Id.male);
            female = FindViewById<RadioButton>(Resource.Id.female);
            male.Checked = true;
            enter.Click += NextLayout;
            male.Click += Male;
            female.Click +=Female;

        }

        /// <summary>
        /// Choosing male gender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Male(object sender, EventArgs e)
        {
            gender = true;
            female.Checked = false;
        }

        /// <summary>
        /// Choosing female gender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Female(object sender, EventArgs e)
        {
            gender = false;
            male.Checked = false;
        }

        /// <summary>
        /// Calculate needed calorie content of the diet and go to geting the diet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextLayout(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(FindViewById<EditText>(Resource.Id.age).Text, out age) && age > 17 && age < 100)
                {
                    if (int.TryParse(FindViewById<EditText>(Resource.Id.Height).Text, out height) && height > 54 && height < 300)
                    {
                        if (int.TryParse(FindViewById<EditText>(Resource.Id.Weight).Text, out weight) && weight > 0 && weight < 300)
                        {
                            if (gender)
                                cal = 10 * weight + 6.25 * height - 5 * age + 5;
                            else
                                cal = 10 * weight + 6.25 * height - 5 * age - 161;
                            GetAPI();
                        }
                        else
                        {
                            Toast.MakeText(this, "Введите вес больше 0 и меньше 300 кг", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Введите рост больше 54 и меньше 300 см", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Введите возраст больше 17 и меньше 100 лет", ToastLength.Long).Show();
                }
            }
            catch (Exception)
            {
                Toast.MakeText(this, "ОШИБКА!", ToastLength.Long).Show();
            }
        }

        /// <summary>
        /// API request
        /// </summary>
        public async void GetAPI()
        {
            if (cal > 305)
            {
                Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                .SetContext(this)
                .SetMessage("Please wait...")
                .Build();
                if (!dialog.IsShowing)
                    dialog.Show();
                string kcal = cal.ToString().Replace(",", ".");
                string url = $"https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/mealplans/generate?targetCalories=" + kcal + "&timeFrame=day";
                HttpResponseMessage response;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "01c79e8e04msh077fcd2b054ada6p1f3778jsn478f5faa097b");
                response = await client.GetAsync(url);
                meals = JsonConvert.DeserializeObject<Meals>(await response.Content.ReadAsStringAsync());
                Intent nextActivity = new Intent(this, typeof(GetDiet));
                nextActivity.PutExtra("calories", cal.ToString());
                nextActivity.PutExtra("bname", meals.meals[0].title);
                nextActivity.PutExtra("btime", meals.meals[0].readyInMinutes);
                nextActivity.PutExtra("lname", meals.meals[1].title);
                nextActivity.PutExtra("ltime", meals.meals[1].readyInMinutes);
                nextActivity.PutExtra("dname", meals.meals[2].title);
                nextActivity.PutExtra("dtime", meals.meals[2].readyInMinutes);
                if (dialog.IsShowing)
                    dialog.Dismiss();
                StartActivity(nextActivity);
            }
            else
                Toast.MakeText(this, "Ошибка вычисления!", ToastLength.Long).Show();
        }
    }
}
using System;
using System.Collections.Generic;
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
    [Activity(Label = "ChooseBrand")]
    public class ChooseBrand : Activity
    {
        string item_name;
        int amount;
        int kcal;
        RootObject root;
        ListView list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.chooseBrand);
            item_name = Intent.GetStringExtra("item_name" ?? "Not recv");
            amount = Intent.GetIntExtra("amount" ?? "Not recv", 0);
            kcal = Intent.GetIntExtra("kcal" ?? "Not recv", 0);
            list = FindViewById<ListView>(Resource.Id.brandlist);
            GetBrands();
            list.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(GetIndex);
        }

        /// <summary>
        /// Get brands' id from API
        /// </summary>
        private async void GetBrands()
        {
            Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                .SetContext(this)
                .SetMessage("Please wait...")
                .Build();
            if (!dialog.IsShowing)
                dialog.Show();
            string url = "https://api.nutritionix.com/v1_1/search/" + item_name + "?results=0%3A20&cal_min=0&cal_max=50000&fields=item_name%2Cbrand_name%2Citem_id%2Cbrand_id&appId=a76fc23d&appKey=7ca7f97a063b60691f2446b4a5b3f543";
            HttpResponseMessage response;
            HttpClient client = new HttpClient();
            response = await client.GetAsync(url);
            root = JsonConvert.DeserializeObject<RootObject>(await response.Content.ReadAsStringAsync());
            List<string> pList = new List<string>();
            foreach (var elem in root.hits)
            {
                pList.Add(elem.fields.brand_name);
            }
            if (pList.Count == 0)
            {
                Toast.MakeText(this, "Ничего не найдено!", ToastLength.Long).Show();
                Intent back = new Intent(this, typeof(CalcSet));
                StartActivity(back);
            }
            else
            {
                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, pList);
                list.Adapter = adapter;
            }
            if (dialog.IsShowing)
                dialog.Dismiss();
        }

        /// <summary>
        /// Go to choosing the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetIndex(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listview = sender as ListView;
            Intent nextlayout = new Intent(this, typeof(ChooseProduct));
            nextlayout.PutExtra("amount", amount);
            nextlayout.PutExtra("item_name", item_name);
            nextlayout.PutExtra("kcal", kcal);
            nextlayout.PutExtra("id", root.hits[e.Position].fields._id);
            StartActivity(nextlayout);
        }
    }
}
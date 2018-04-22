using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace FutureRecommenderApp
{
    class deleteSkillAdapter : BaseAdapter<GetUserSkills_Result>
    {
        Activity context;
        List<GetUserSkills_Result> list;
        String Email;


        public deleteSkillAdapter(Activity _context, List<GetUserSkills_Result> _list, string email) : base()
        {
            this.Email = email;
            this.context = _context;

            this.list = _list;




        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override GetUserSkills_Result this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.Add_Hobby_List_Item, parent, false);

            GetUserSkills_Result item = this[position];
            view.FindViewById<TextView>(Resource.Id._name_).Text = item.name;

            Button v = view.FindViewById<Button>(Resource.Id._add_);
            v.Click += (sender, e) => {
                try
                {
                    Task<string> task = DeleteSkill(Email, item.name);
                    var x = JsonConvert.DeserializeObject<int>(task.Result);
                    v.Enabled = false;
                }
                catch
                {

                }
            };
            return view;
        }

        private async Task<string> DeleteSkill(string email, string name)
        {
            var client = new HttpClient();





            string r = "2";
            var jsonRequest = new { email = email, value = name };

            var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
            HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("http://futurerecommend.azurewebsites.net/api/delete/skills", content).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                r = await result.Content.ReadAsStringAsync();
            }
            return r;
        }
    }
}
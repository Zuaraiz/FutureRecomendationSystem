using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using V7Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Preferences;

namespace FutureRecommenderApp
{
    [Activity(Label = "Future Councelor", Theme = "@android:style/Theme.Holo.Light.DarkActionBar")]
    public class Dashboard : TabActivity 
    {
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.dashboard);
            ActionBar.SetHomeButtonEnabled(true);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetIcon(Resource.Drawable.logout);

            CreateTab(typeof(Recommendation), "Recommendations", "Recommendations");
            CreateTab(typeof(Profile), "Profile", "Profile");
           

        }
        private void CreateTab(Type activityType, string tag, string label)
        {
            var intent = new Intent(this, activityType);
            intent.AddFlags(ActivityFlags.NewTask);

            var spec = TabHost.NewTabSpec(tag);
            spec.SetIndicator(label);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(ApplicationContext);
                    ISharedPreferencesEditor editor = prefs.Edit();
                    editor.Remove("email");
                    editor.Commit();
                    editor.Clear();
                    editor.Apply();
                    StartActivity(typeof(MainActivity));
                   
                    this.Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

    }
}
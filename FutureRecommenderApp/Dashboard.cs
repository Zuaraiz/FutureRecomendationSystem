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

namespace FutureRecommenderApp
{
    [Activity(Label = "Dashboard", Theme = "@android:style/Theme.Holo.Light.DarkActionBar")]
    public class Dashboard : TabActivity
    {
       
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.dashboard);
         
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

    }
}
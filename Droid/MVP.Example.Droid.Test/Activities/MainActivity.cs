using System;
using Xamarin.UITest;

namespace MVP.Example.Droid.Test.Activities
{
    public class MainActivity : BaseActivity
    {

        public MainActivity(IApp app, string pageTitle)
            :base(app, pageTitle)
        {
        }

        public void TapAddButton()
        {
            App.Tap(x => x.Marked("btn_add"));
            App.Screenshot("Add Button Tapped");
        }

        public void TapDeleteAllItems()
        {
            App.Tap(x => x.Marked("More options"));
            App.Tap(x => x.Marked("Delete values"));
        }
    }
}

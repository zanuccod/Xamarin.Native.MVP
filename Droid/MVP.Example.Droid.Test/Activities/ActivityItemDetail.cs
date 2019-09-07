using System;
using Xamarin.UITest;

namespace MVP.Example.Droid.Test.Activities
{
    public class ActivityItemDetail : BaseActivity
    {
        public ActivityItemDetail(IApp app, string pageTitle)
            :base(app, pageTitle)
        {
        }

        public void EnterNameText(string text)
        {
            App.EnterText(x => x.Marked("item_name"), text);
            App.DismissKeyboard();
            App.Screenshot("Entered Name");
        }

        public void EnterCountryText(string text)
        {
            App.EnterText(x => x.Marked("item_country"), text);
            App.DismissKeyboard();
            App.Screenshot("Entered Country");
        }

        public void EnterBornDateText(string text)
        {
            App.EnterText(x => x.Marked("item_born_date"), text);
            App.DismissKeyboard();
            App.Screenshot("Entered Born Date");
        }

        public void TapSaveButton()
        {
            App.Tap(x => x.Marked("btn_save"));
            App.Screenshot("Save Button Tapped");
        }

        public void TapActionBarUpButton()
        {
            App.Tap(x => x.Marked("Navigate up"));
            App.Screenshot("Action Bar Up Button Tapped");
        }
    }
}

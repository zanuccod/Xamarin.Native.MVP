using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace MVP.Example.Droid.Test.Activities
{
    [TestFixture]
    public class MainActivityTest
    {
        IApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp();

            // wait for main activity
            app.WaitForElement("activity_main");
            app.Screenshot("App Launched");
        }

        [TearDown]
        public void AfterEachTest()
        {
            try
            {
                app.WaitForElement("activity_main");
            }
            catch
            {
                app.Tap(x => x.Marked("Navigate up"));
            }

            app.Tap(x => x.Marked("More options"));
            app.Tap(x => x.Marked("Delete values"));
        }

        /*
        [Test]
        public void RelpTest()
        {
            app.Repl();
        }
        */

        [Test]
        public void AddBtn_SaveNewItem_Success()
        {
            // Act => do in setup method

            // press add button
            app.Tap(x => x.Marked("btn_add"));
            app.Screenshot("Add Button Tapped");

            // wait for activity item detail
            app.WaitForElement("activity_item_detail");

            // enter the name
            app.EnterText(x => x.Marked("item_name"), "test_name");
            app.DismissKeyboard();
            app.Screenshot("Entered Name");

            // enter the country
            app.EnterText(x => x.Marked("item_country"), "test_country");
            app.DismissKeyboard();
            app.Screenshot("Entered Country");

            // enter the born date
            app.EnterText(x => x.Marked("item_born_date"), "10-10-2000");
            app.DismissKeyboard();
            app.Screenshot("Entered Born Date");

            // press save button
            app.Tap(x => x.Marked("btn_save"));
            app.Screenshot("Save Button Tapped");

            // wait for main_activity
            app.WaitForElement("activity_main");

            // Assert => entered item should be on the list
            Assert.True(DoesItemExist("test_name"));
        }

        [Test]
        public void AddBtn_PressReturnBtn_Success()
        {
            // Act

            // press add button
            app.Tap(x => x.Marked("btn_add"));
            app.Screenshot("Add Button Tapped");

            // wait for activity item detail
            app.WaitForElement("activity_item_detail");

            // press return button
            app.Tap(x => x.Marked("Navigate up"));
            app.Screenshot("Return Button Tapped");

            // wait for main_activity
            app.WaitForElement("activity_main");

            // Assert => entered item should be on the list
            Assert.False(DoesItemExist("test_name"));
        }

        private bool DoesItemExist(string name)
        {
            try
            {
                app.ScrollDownTo(name);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

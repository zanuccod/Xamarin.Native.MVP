using System;
using NUnit.Framework;
using Xamarin.UITest;
using System.Threading.Tasks;
using MVP.Example.Droid.Test.Activities;

namespace MVP.Example.Droid.Test.Test
{
    public abstract class BaseTest
    {
        #region Properties

        protected IApp App { get; private set; }
        protected MainActivity MainActivity { get; private set; }
        protected ActivityItemDetail ActivityItemDetail { get; private set; }

        #endregion

        #region Methods

        [SetUp]
        public async Task TestSetup()
        {
            App = AppInitializer.StartApp();

            MainActivity = new MainActivity(App, "activity_main");
            ActivityItemDetail = new ActivityItemDetail(App, "activity_item_detail");

            await MainActivity.WaitForPageToLoad().ConfigureAwait(false);
            App.Screenshot("App Launched");
        }

        [TearDown]
        public async Task TestTearDown()
        {
            try
            {
                await Task.FromResult(App.WaitForElement("activity_main")).ConfigureAwait(false);
            }
            catch
            {
                ActivityItemDetail.TapActionBarUpButton();
            }

            MainActivity.TapDeleteAllItems();
        }

        #endregion

        #region Protected Methods

        protected bool DoesItemExist(string name)
        {
            try
            {
                App.ScrollDownTo(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}

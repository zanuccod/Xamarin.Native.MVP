using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MVP.Example.Droid.Test.Test
{
    [TestFixture]
    public class Tests : BaseTest
    {
        [Test]
        public async Task AddBtn_SaveNewItem_Success()
        {
            // Arrange

            // Act
            // press add button
            MainActivity.TapAddButton();

            // wait for activity item detail
            await Task.FromResult(App.WaitForElement("activity_item_detail")).ConfigureAwait(false);

            // enter item data
            ActivityItemDetail.EnterNameText("test_name");
            ActivityItemDetail.EnterCountryText("test_country");
            ActivityItemDetail.EnterBornDateText("10-10-2000");

            // press save button
            ActivityItemDetail.TapSaveButton();

            // wait for main_activity
            await Task.FromResult(App.WaitForElement("activity_main")).ConfigureAwait(false);

            // Assert => entered item should be on the list
            Assert.True(DoesItemExist("test_name"));
        }

        [Test]
        public async Task AddBtn_PressReturnBtn_Success()
        {
            // Arrange

            // Act
            // press add button
            MainActivity.TapAddButton();

            // wait for activity item detail
            await Task.FromResult(App.WaitForElement("activity_item_detail")).ConfigureAwait(false);

            // press return button
            ActivityItemDetail.TapActionBarUpButton();

            // wait for main_activity
            await Task.FromResult(App.WaitForElement("activity_main")).ConfigureAwait(false);

            // Assert => entered item should be on the list
            Assert.False(DoesItemExist("test_name"));
        }

        [Test]
        public async Task ViewSavedElement_Success()
        {
            // Arrange => save new item

            var name = "view_test_name";
            var country = "view_test_country";
            var bornDate = "11-11-1111";

            // press add button
            MainActivity.TapAddButton();

            // wait for activity item detail
            await Task.FromResult(App.WaitForElement("activity_item_detail")).ConfigureAwait(false);

            // enter item data
            ActivityItemDetail.EnterNameText(name);
            ActivityItemDetail.EnterCountryText(country);
            ActivityItemDetail.EnterBornDateText(bornDate);

            // press save button
            ActivityItemDetail.TapSaveButton();

            // wait for main_activity
            await Task.FromResult(App.WaitForElement("activity_main")).ConfigureAwait(false);

            // Act
            App.Tap(x => x.Marked(name));

            // wait for activity_item_detail
            await Task.FromResult(App.WaitForElement("activity_item_detail")).ConfigureAwait(false);

            // Assert
            Assert.AreEqual(App.Query(x => x.Marked("item_name")).FirstOrDefault().Text, name);
            Assert.AreEqual(App.Query(x => x.Marked("item_country")).FirstOrDefault().Text, country);
            Assert.AreEqual(App.Query(x => x.Marked("item_born_date")).FirstOrDefault().Text, bornDate);
        }
    }
}

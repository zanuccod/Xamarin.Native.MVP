using Common.Entities;
using Common.IViews;
using Common.Models;
using Common.Presenters;
using Moq;
using NUnit.Framework;

namespace Common.Test.Presenters
{
    [TestFixture]
    public class ItemsDetailViewPresenterTest
    {
        Mock<IDataStore<Student>> mockModel;
        private IDataStore<Student> model;

        Mock<IItemDetailView> mockView;
        private IItemDetailView view;

        private ItemsDetailViewPresenter<Student> presenter;

        [SetUp]
        public void BeforeEachTest()
        {
            mockModel = new Mock<IDataStore<Student>>();
            model = mockModel.Object;

            mockView = new Mock<IItemDetailView>();
            view = mockView.Object;

            presenter = new ItemsDetailViewPresenter<Student>(view, model);
        }

        [Test]
        public void Constructor_NotNullElements_Success()
        {
            // Assert
            Assert.NotNull(presenter.AddItemCommand);
        }

        [Test]
        public void AddItemAsync_AddItem_Success()
        {
            // Arrange
            var expectedResult = new Student() { Name = "name", BornDate = "01-01-1970", Country = "country_test" };

            // mock AddItemAsync() method
            mockModel.Setup(x => x.AddItemAsync(expectedResult));

            // Act
            presenter.AddItemCommand.Execute(expectedResult);

            // Assert
            Assert.False(presenter.IsBusy);
        }

        /// <summary>
        /// If isBusy true presenter.AddItemCommand nothing to do and at the end isBusy is still true
        /// </summary>
        [Test]
        public void AddItemAsync_IsBusyTrue_NothingToDo()
        {
            // Arrange
            presenter.IsBusy = true;
            var item = new Student() { Name = "name", BornDate = "01-01-1970", Country = "country_test" };

            // mock AddItemAsync() method
            mockModel.Setup(x => x.AddItemAsync(item));

            // Act
            presenter.AddItemCommand.Execute(item);

            // Assert
            Assert.True(presenter.IsBusy);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Common.ViewModels;
using Moq;
using NUnit.Framework;

namespace Common.Test.ViewModels
{
    public class MockEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BornDate { get; set; }
        public string Country { get; set; }
    }

    [TestFixture]
    public class ItemsViewModelTests
    {
        Mock<IDataStore<MockEntity>> mockDataStore;
        private IDataStore<MockEntity> dataStore;
        private ItemsViewModel<MockEntity> viewModel;

        [SetUp]
        public void BeforeEachTest()
        {
            mockDataStore = new Mock<IDataStore<MockEntity>>();
            dataStore = mockDataStore.Object;

            viewModel = new ItemsViewModel<MockEntity>(dataStore);
        }

        [Test]
        public void Constructor_NotNullElements_Success()
        {
            // Assert
            Assert.NotNull(viewModel.Items);
            Assert.AreEqual(0, viewModel.Items.Count);
            Assert.NotNull(viewModel.AddItemCommand);
            Assert.NotNull(viewModel.LoadItemsCommand);
        }

        [Test]
        public void LoadItemsCommand_ReadTwoElements_Success()
        {
            // Arrange
            var expectedResult = new List<MockEntity>
            {
                new MockEntity() { Name = "name", BornDate = "01-01-1970", Country = "country_test" },
                new MockEntity() { Name = "name_1", BornDate = "02-02-1971", Country = "country_test_1" }
            };

            // mock expected result of GetItemsAsync() method
            mockDataStore.Setup(x => x.GetItemsAsync()).Returns(Task.FromResult(expectedResult));

            // Act
            viewModel.LoadItemsCommand.Execute(null);

            // Assert
            Assert.AreEqual(expectedResult, viewModel.Items);
        }

        [Test]
        public void AddItemAsync_AddItem_Success()
        {
            // Arrange
            var expectedResult = new MockEntity() { Name = "name", BornDate = "01-01-1970", Country = "country_test" };

            // mock AddItemAsync() method
            mockDataStore.Setup(x => x.AddItemAsync(expectedResult));

            // Act
            viewModel.AddItemCommand.Execute(expectedResult);

            // Assert
            Assert.True(expectedResult.Equals(viewModel.Items.First()));
        }

        [Test]
        public void DeleteAllAsync_DeleteAllOnNotEmptyList_Success()
        {
            // Arrange
            var items = new List<MockEntity>
            {
                new MockEntity() { Name = "name", BornDate = "01-01-1970", Country = "country_test" },
                new MockEntity() { Name = "name_1", BornDate = "02-02-1971", Country = "country_test_1" },
                new MockEntity() { Name = "name_2", BornDate = "02-02-1972", Country = "country_test_2" }
            };

            // mock AddItemAsync() method for generic input object of MockEntity class
            mockDataStore.Setup(x => x.AddItemAsync(It.IsAny<MockEntity>()));

            items.ForEach(x => viewModel.AddItemCommand.Execute(x));
            Assert.AreEqual(items.Count, viewModel.Items.Count);

            // mock DeleteAllAsync() method
            mockDataStore.Setup(x => x.DeleteAllAsync());

            // Act
            viewModel.DeleteAllCommand.Execute(null);

            // Assert

            // items list of the view model should be empty
            Assert.AreEqual(0, viewModel.Items.Count);
        }
    }
}
